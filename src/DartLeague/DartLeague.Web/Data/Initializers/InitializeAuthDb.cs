using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DartLeague.Web.Data.Initializers
{
    public static class InitializeAuthDb
    {
        public static async Task Initialize(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetService<AuthDbContext>();
            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var adminRole = new IdentityRole("Administrator");
                await roleStore.CreateAsync(adminRole);

                //await roleStore.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, "account.manage"));
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var userRole = new IdentityRole("User");
                await roleStore.CreateAsync(userRole);

                //await roleStore.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, "account.manage"));
            }


            if (!context.Roles.Any(r => r.Name == "Manager"))
            {
                var userRole = new IdentityRole("Manager");
                await roleStore.CreateAsync(userRole);

                //await roleStore.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, "account.manage"));
            }
        }
    }
}