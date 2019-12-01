using DartLeague.Domain.BrowsableFiles;
using DartLeague.Infrastructure.BrowsableFiles;
using DartLeague.Web.Configurations;
using DartLeague.Web.Data;
using DartLeague.Web.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mindscape.Raygun4Net.AspNetCore;
using System;
using System.Linq;
using System.Reflection;

namespace DartLeague.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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

            services.AddAuthDbContext(
                authDbConnString,
                migrationsAssembly);

            services.AddSeasonDbContext(
                trentonDartsDbConnString,
                migrationsAssembly);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AuthDbContext>();

            services.AddTransient<IBrowsableFileService, FileSystemBrowsableFileService>();

            services.Configure<BrowsableFileOptions>(Configuration.GetSection("BrowsableFile"));

            //services.AddMvc();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddRaygun(Configuration, new RaygunMiddlewareSettings()
            {
                ClientProvider = new DartLeagueRaygunAspNetCoreClientProvider()
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
