using ExpenseManagement.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Data
{
    class Seeder
    {
        public async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "Regular" };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                List<AppUser> users = new List<AppUser>
                {
                    new AppUser
                    {
                        FirstName = "John",
                        LastName = "James",
                        Email = "JJ@gmail.com",
                        UserName = "jjgh",
                        PhoneNumber = "080479379494",
                        Department = "Financial Control"
                    },
                    new AppUser
                    {
                        FirstName = "Anne",
                        LastName = "Perry",
                        Email = "Anne@gmail.com",
                        UserName = "Annie",
                        PhoneNumber = "080743979494",
                        Department = "Information Technology"
                    },
                    new AppUser
                    {
                        FirstName = "Glenna",
                        LastName = "Waters",
                        Email = "glennawaters@roughies.com",
                        UserName = "GlenW",
                        PhoneNumber =  "+1 (848) 443-2870",
                        Department = "Operations"

                    },
                    new AppUser
                    {
                        FirstName = "Vonda",
                        LastName = "Ramsey",
                        Email = "vondaramsey@roughies.com",
                        UserName = "VodaR",
                        PhoneNumber = "+1 (828) 400-3241",
                        Department = "Sales"
                    },

                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "P@ssw0rd");
                    if (user == users[0])
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, "Regular");
                    }

                }

            }
        }
    }
}
