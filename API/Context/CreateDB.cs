using API.Context;
using API.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CSWork21.Data
{
    public static class CreateDB
    {
        public static void TryInit(DBContext dbcontext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, bool withTestData = false)
        {
            bool hasCreated = dbcontext.Database.EnsureCreated();

            if (hasCreated && withTestData)
            {
                Task creator = CreateRoles(roleManager, userManager);
                creator.Wait();

                dbcontext.SaveChanges();
            }
        }


        private async static Task<bool> CreateRoles(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            await roleManager.CreateAsync(new IdentityRole("user"));
            await roleManager.CreateAsync(new IdentityRole("administrator"));

            await CreateUser(userManager, "user", "1234", "user");
            await CreateUser(userManager, "admin", "1234", "administrator");
            return true;
        }

        private async static Task<bool> CreateUser(UserManager<AppUser> userManager, string login, string password, string role)
        {
            var user = new AppUser { UserName = login, DisplayName = $"N{login}" };
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, role);
            return true;
        }
    }

    
}
