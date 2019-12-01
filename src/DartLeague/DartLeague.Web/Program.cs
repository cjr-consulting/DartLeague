using DartLeague.Web.Configurations;
using DartLeague.Web.Data.Initializers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DartLeague.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                scope.UseLeagueDbMigrations();

                try
                {
                    // Requires using RazorPagesMovie.Models;
                    // SeedData.Initialize(services);
                    InitializeAuthDb.Initialize(scope).Wait();
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
