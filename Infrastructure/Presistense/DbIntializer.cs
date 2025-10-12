using Domain;
using Presistense.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Presistense.Identity;
using Microsoft.AspNetCore.Identity;
using Domain.Modules.Identity;

namespace Presistense { 
    public class DbIntializer(MetroDbContex metroDbContex,UsersDbContext usersDb,UserManager<AppUsers>userManager,RoleManager<IdentityRole>roleManager) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            if ((await metroDbContex.Database.GetPendingMigrationsAsync()).Any())
            {
                await metroDbContex.Database.MigrateAsync();
            }
        }

        public async Task InitializeIdentityAsync()
        {
            if (usersDb.Database.GetPendingMigrations().Any())
            {
                await usersDb.Database.MigrateAsync();
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("ViceAdmin"))
                await roleManager.CreateAsync(new IdentityRole("ViceAdmin"));

            var superAdmin = await userManager.FindByNameAsync("Admin");
            if (superAdmin == null)
            {
                superAdmin = new AppUsers
                {
                    DisplayName = "Admin",
                    PhoneNumber = "01060512789",
                    UserName = "Admin"
                };

                var passwordAdmin = "Admin700";
                var createResult = await userManager.CreateAsync(superAdmin, passwordAdmin);

                if (createResult.Succeeded)
                    await userManager.AddToRoleAsync(superAdmin, "Admin");
            }

            var viceAdmin = await userManager.FindByNameAsync("ViceAdmin");
            if (viceAdmin == null)
            {
                viceAdmin = new AppUsers
                {
                    DisplayName = "ViceAdmin",
                    PhoneNumber = "01111128427",
                    UserName = "ViceAdmin"
                };

                var passwordVice = "Admin757";
                var createResult = await userManager.CreateAsync(viceAdmin, passwordVice);

                if (createResult.Succeeded)
                    await userManager.AddToRoleAsync(viceAdmin, "ViceAdmin");
            }
        }
    }
}