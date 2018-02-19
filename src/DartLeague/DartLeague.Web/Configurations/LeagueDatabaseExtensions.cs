﻿using DartLeague.Repositories.LeagueData;
using DartLeague.Repositories.SeasonData;
using DartLeague.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DartLeague.Web.Configurations
{
    public static class LeagueDatabaseExtensions
    {
        public static void AddLeagueDbContext(this IServiceCollection services, string connectionString, string migrationsAssembly)
        {
            services.AddDbContext<LeagueContext>(options =>
            options.UseSqlServer(
                connectionString,
                b => b.MigrationsAssembly(migrationsAssembly)
                )
            );
        }

        public static void AddSeasonDbContext(this IServiceCollection services, string connectionString, string migrationsAssembly)
        {
            services.AddDbContext<SeasonContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly(migrationsAssembly)
                )
            );
        }

        public static void UseLeagueDbMigrations(this IServiceScope serviceScope)
        {
            var leagueContext = serviceScope.ServiceProvider.GetService<LeagueContext>();
            leagueContext.Database.Migrate();

            var authDbContext = serviceScope.ServiceProvider.GetService<AuthDbContext>();
            authDbContext.Database.Migrate();

            var seasonContext = serviceScope.ServiceProvider.GetService<SeasonContext>();
            seasonContext.Database.Migrate();
        }
    }
}
