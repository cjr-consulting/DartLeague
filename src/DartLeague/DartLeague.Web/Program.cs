using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DartLeague.Web.Configurations;
using DartLeague.Web.Data.Initializers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DartLeague.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
            */
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                scope.UseLeagueDbMigrations();

                try
                {
                    // Requires using RazorPagesMovie.Models;
                    // SeedData.Initialize(services);
                    InitializeAuthDb.Initialize(scope).Wait();
                    InitializeIdentityDb.Initialize(scope);
                    InitializeLeagueDb.Initialize(scope);
                    InitializeSeasonDb.InitializeAsync(scope).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .Build();
    }
}