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
            if (!userManager.Users.Any())
            {
                var SuperAdmin = new AppUsers
                {
                    DisplayName = "Admin",
                    PhoneNumber="01060512789",
                    UserName="Admin"
                };
                var ViceAdmin = new AppUsers
                {
                    DisplayName="ViceAdmin",
                    PhoneNumber="01111128427",
                    UserName="ViceAdmin"
                };
                var PasswordAdmin = "Admin700";
                var PasswordVice = "Admin757";
                await userManager.AddToRoleAsync(SuperAdmin, "Admin");
                await userManager.AddToRoleAsync(ViceAdmin, "ViceAdmin");
                await userManager.CreateAsync(SuperAdmin, PasswordAdmin);
                await userManager.CreateAsync(ViceAdmin, PasswordVice);
            }
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin",
                });
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name="ViceAdmin",
                });
            }
        }
    }
}