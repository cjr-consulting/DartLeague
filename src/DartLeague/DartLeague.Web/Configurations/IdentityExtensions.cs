﻿using System;
using DartLeague.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DartLeague.Web.Configurations
{
    public static class IdentityExtensions
    {
        public static void AddIdentityServerConfiguration(this IServiceCollection services, string authSqlConnectionString, string migrationsAssembly)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                
                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(
                    authSqlConnectionString,
                    b => b.MigrationsAssembly(migrationsAssembly)
                )
            );
        }
    }
}