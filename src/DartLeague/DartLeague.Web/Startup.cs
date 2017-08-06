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

namespace DartLeague.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BrowsableFileOptions>(Configuration.GetSection("BrowsableFile"));

            var authSqlConnectionString = Configuration.GetConnectionString("AuthMySqlProvider");
            
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            
            services.AddIdentityServerConfiguration(
                Configuration.GetConnectionString("AuthMySqlProvider"),
                migrationsAssembly);

            services.AddLeagueDbContext(
                Configuration.GetConnectionString("LeagueMySqlProvider"),
                migrationsAssembly);
            
            services.AddWinterSeasonDbContext(
                Configuration.GetConnectionString("WinterSeasonMySqlProvider"),
                migrationsAssembly);

            services.AddSeasonDbContext(
                Configuration.GetConnectionString("SeasonMySqlProvider"),
                migrationsAssembly);

            services.AddIdentity<UserIdentity, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            // Add framework services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IBrowsableFileService, FileSystemBrowsableFileService>();

            services.AddMvc();
            
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddConfigurationStore(builder =>
                    builder.UseMySql(authSqlConnectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(builder =>
                    builder.UseMySql(authSqlConnectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)))
                .AddAspNetIdentity<UserIdentity>();
            
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
            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });

            app.UseLeagueDbMigrations();
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

            InitializeAuthDb.Initialize(app).Wait();
            InitializeIdentityDb.Initialize(app);
            InitializeLeagueDb.Initialize(app).Wait();

            app.UseStaticFiles();

            app.UseIdentity();
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
