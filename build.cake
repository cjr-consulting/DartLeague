#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#tool "nuget:?package=OctopusTools"
#addin "Cake.Docker"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var sourceTag = Argument("sourceTag", "latest");
var deployTag = Argument("deployTag", "latest");
//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./src/DartLeague/obj/bin") + Directory(configuration);
var buildDev = Directory("./src/DartLeague/DartLeague.Web/obj/Docker/Publish");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDev);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./src/DartLeague/DartLeague.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    /*
    var exitCodeWithArgument = StartProcess(
        "docker-compose",
        new ProcessSettings {
            Arguments = "-f ./src/dartleague/docker-compose.ci.build.yml up" 
        });
        */
    DockerComposeUp(new DockerComposeUpSettings()
    {
        Files = new string[] {"./src/dartleague/docker-compose.ci.build.yml"}
    });
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3("./src/**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
        NoResults = true
        });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() => {
    });

Task("Debug")
    .IsDependentOn("Build")
    .Does(() => {
        DockerComposeKill(new DockerComposeKillSettings(){
            Files = new string[]{
                "./src/dartleague/docker-compose.yml",
                "./src/dartleague/docker-compose.override.yml",
                "./src/dartleague/docker-compose.vs.debug.yml"
            },
            ProjectName = "dartleagueweb"
        });
        DockerComposeUp(new DockerComposeUpSettings(){
            Files = new string[]{
                "./src/dartleague/docker-compose.yml",
                "./src/dartleague/docker-compose.override.yml",
                "./src/dartleague/docker-compose.vs.debug.yml"
            },
            ProjectName = "dartleagueweb"
        });
    });

Task("Register")
    .IsDependentOn("Build")
    .Does(() => {
        DockerComposeBuild(new DockerComposeBuildSettings(){
            Files = new string[]{
                "./src/dartleague/docker-compose.yml",
                "./src/dartleague/docker-compose.override.yml",
                "./src/dartleague/docker-compose.vs.release.yml"
            },
            ProjectName = "dartleagueweb",
            NoCache = true
        });
        DockerTag("dartleagueweb:" + sourceTag, "registry.thecitizens.net/dartleagueweb:" + deployTag);
        DockerTag("registry.thecitizens.net/dartleagueweb:" + deployTag, "registry.thecitizens.net/dartleagueweb:latest");
        DockerPush("registry.thecitizens.net/dartleagueweb:latest");
    });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
