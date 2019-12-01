using DartLeague.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DartLeague.Web.Configurations
{
    public static class IdentityExtensions
    {
        public static void AddAuthDbContext(this IServiceCollection services, string authSqlConnectionString, string migrationsAssembly)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(
                    authSqlConnectionString,
                    b => b.MigrationsAssembly(migrationsAssembly)
                )
            );
        }
    }
}