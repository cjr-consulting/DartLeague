using DartLeague.Repositories.LeagueData;
using DartLeague.Repositories.WinterSeasonData;
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
            options.UseMySql(
                connectionString,
                b => b.MigrationsAssembly(migrationsAssembly)
                )
            );
        }

        public static void AddWinterSeasonDbContext(this IServiceCollection services, string connectionString, string migrationsAssembly)
        {
            services.AddDbContext<WinterSeasonContext>(options =>
            options.UseMySql(
                connectionString,
                b => b.MigrationsAssembly(migrationsAssembly)
                )
            );
        }

        public static void UseLeagueDbMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var authDbContext = serviceScope.ServiceProvider.GetService<AuthDbContext>();
                authDbContext.Database.Migrate();

                var leagueContext = serviceScope.ServiceProvider.GetService<LeagueContext>();
                leagueContext.Database.Migrate();

                var winterSeasonContext = serviceScope.ServiceProvider.GetService<WinterSeasonContext>();
                winterSeasonContext.Database.Migrate();
            }
        }
    }
}
