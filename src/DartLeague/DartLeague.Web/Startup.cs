using System;
using System.Reflection;
using DartLeague.Domain.BrowsableFiles;
using DartLeague.Infrastructure.BrowsableFiles;
using DartLeague.Web.Configurations;
using DartLeague.Web.Data;
using DartLeague.Web.Data.Initializers;
using DartLeague.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mindscape.Raygun4Net;
using DartLeague.Web.Helpers;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using Mindscape.Raygun4Net.AspNetCore;

namespace DartLeague.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {                        
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var trentonDartsDbConnString = Configuration.GetConnectionString("TrentonDartsDb");
            var authDbConnString = Configuration.GetConnectionString("AuthDb");

            services.AddLeagueDbContext(
                trentonDartsDbConnString,
                migrationsAssembly);

            services.AddIdentityServerConfiguration(
                authDbConnString,
                migrationsAssembly);
            
            services.AddSeasonDbContext(
                trentonDartsDbConnString,
                migrationsAssembly);

            services.AddIdentity<UserIdentity, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            // Add framework services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IBrowsableFileService, FileSystemBrowsableFileService>();

            services.Configure<BrowsableFileOptions>(Configuration.GetSection("BrowsableFile"));

            services.AddMvc();

            //var authSqlConnectionString = Configuration.GetConnectionString("AuthMySqlProvider");
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(authDbConnString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(authDbConnString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddAspNetIdentity<UserIdentity>();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.Cookie.Name = "mvchybrid";
                    options.LoginPath = "/Account/LogIn";
                    options.LogoutPath = "/Account/LogOff";
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    
                    options.ResponseType = "code id_token";

                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";

                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role,
                    };
                });

            services.AddRaygun(Configuration, new RaygunMiddlewareSettings()
                {
                    ClientProvider = new DartLeagueRaygunAspNetCoreClientProvider()
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseRaygun();
            }
            
            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
