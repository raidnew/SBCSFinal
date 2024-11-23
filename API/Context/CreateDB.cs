using API.Context;
using API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CSWork21.Data
{
    public static class CreateDB
    {
        public static async Task TryInitAsync(DBContext dbcontext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, bool withTestData = false)
        {
            try
            {
                bool hasCreated = dbcontext.Database.EnsureCreated();


                if (hasCreated && withTestData)
                {
                    //await CreateRoles(roleManager, userManager);

                    Task creator = CreateRoles(roleManager, userManager);
                    creator.Wait();

                    CreateOrdersStatuses(dbcontext);
                    CreateOrders(dbcontext);
                    CreateProjects(dbcontext);
                    CreateBlogs(dbcontext);
                    CreateServices(dbcontext);

                    dbcontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private async static Task CreateRoles(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            await roleManager.CreateAsync(new IdentityRole("user"));
            await roleManager.CreateAsync(new IdentityRole("administrator"));

            await CreateUser(userManager, "user", "1234", "user");
            await CreateUser(userManager, "admin", "1234", "administrator");
        }

        private static void CreateOrdersStatuses(DBContext dbcontext)
        {
            dbcontext.OrdersStatuses.Add(new OrdersStatus() { Name = "Got" });
            dbcontext.OrdersStatuses.Add(new OrdersStatus() { Name = "Process" });
            dbcontext.OrdersStatuses.Add(new OrdersStatus() { Name = "Complete" });
            dbcontext.OrdersStatuses.Add(new OrdersStatus() { Name = "Rejected" });
            dbcontext.OrdersStatuses.Add(new OrdersStatus() { Name = "Canceled" });
        }

        private static void CreateOrders(DBContext dbcontext)
        {
            dbcontext.Orders.Add(new Order() { Email = "asd1@asd.asd", Message = "11111", Name = "name1", StatusId = 1 });
            dbcontext.Orders.Add(new Order() { Email = "asd2@asd.asd", Message = "22222", Name = "name2", StatusId = 1 });
            dbcontext.Orders.Add(new Order() { Email = "asd3@asd.asd", Message = "33333", Name = "name3", StatusId = 2 });
            dbcontext.Orders.Add(new Order() { Email = "asd4@asd.asd", Message = "44444", Name = "name4", StatusId = 3 });
            dbcontext.Orders.Add(new Order() { Email = "asd5@asd.asd", Message = "55555", Name = "name5", StatusId = 4 });
            dbcontext.Orders.Add(new Order() { Email = "asd6@asd.asd", Message = "66666", Name = "name6", StatusId = 5 });
        }

        private static void CreateProjects(DBContext dbcontext)
        {
            dbcontext.Projects.Add(new ProjectEntry() { Name = "Project1" });
            dbcontext.Projects.Add(new ProjectEntry() { Name = "Project2" });
            dbcontext.Projects.Add(new ProjectEntry() { Name = "Project3" });
        }

        private static void CreateBlogs(DBContext dbcontext)
        {
            dbcontext.Blogs.Add(new BlogEntry() { Name = "Blog1", ShortDescription = "Shoprt text1", Description = "Long text1 Long text1 Long text1 Long text1 Long text1 Long text1 Long text1" , Date = new DateTime(2002, 3, 3, 3,4,5)});
            dbcontext.Blogs.Add(new BlogEntry() { Name = "Blog2", ShortDescription = "Shoprt text2", Description = "Long text1 Long text1 Long text1 Long text1 Long text1 Long text1 Long text1222" , Date = new DateTime(2012, 4, 24, 3,4,15)});
            dbcontext.Blogs.Add(new BlogEntry() { Name = "Blog3", ShortDescription = "Shoprt text3", Description = "Long text1 Long text1 Long text1 Long text1 Long text1 Long text1 Long text1333" , Date = new DateTime(2022, 5, 14, 3,14,5)});
            dbcontext.Blogs.Add(new BlogEntry() { Name = "Blog4", ShortDescription = "Shoprt text4", Description = "Long text1 Long text1 Long text1 Long text1 Long text1 Long text1 Long text1444" , Date = new DateTime(2014, 6, 4, 13,4,5)});
            dbcontext.Blogs.Add(new BlogEntry() { Name = "Blog5", ShortDescription = "Shoprt text5", Description = "Long text1 Long text1 Long text1 Long text1 Long text1 Long text1 Long text1555" , Date = new DateTime(2012, 7, 6, 3,4,15)});
            dbcontext.Blogs.Add(new BlogEntry() { Name = "Blog6", ShortDescription = "Shoprt text6", Description = "Long text1 Long text1 Long text1 Long text1 Long text1 Long text1 Long text1666" , Date = new DateTime(2012, 8, 16, 3,14,5)});
        }

        private static void CreateServices(DBContext dbcontext)
        {
            dbcontext.Services.Add(new ServiceEntry() { Name = "Service1", Description = "We can do service1"});
            dbcontext.Services.Add(new ServiceEntry() { Name = "Service2", Description = "We can do service2"});
            dbcontext.Services.Add(new ServiceEntry() { Name = "Service3", Description = "We can do service3"});
            dbcontext.Services.Add(new ServiceEntry() { Name = "Service4", Description = "We can do service4"});
            dbcontext.Services.Add(new ServiceEntry() { Name = "Service5", Description = "We can do service5"});
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
