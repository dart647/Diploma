using JournalForSecurity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Data
{
    public class SampleData
    {
        public static async Task InitializeAsync(AppDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!dbContext.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Security"));
                await roleManager.CreateAsync(new IdentityRole("HeadOfDepartment"));
                await roleManager.CreateAsync(new IdentityRole("HeadOfOrganisation"));
            }
            if (!userManager.Users.Any())
            {
                var user = new User()
                {
                    Birthday = new DateTime(),
                    CardEvents = new List<CardEvent>(),
                    CardTasks = new List<CardTask>(),
                    Email = "",
                    ExplanatoryNotes = new List<ExplanatoryNote>(),
                    FirstName = "admin",
                    SecondName = "",
                    ThirdName = "",
                    UserName = "admin"
                };
                var password = "admin";

                var result = await userManager.CreateAsync(user, password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
                
                user = new User()
                {
                    Birthday = new DateTime(),
                    CardEvents = new List<CardEvent>(),
                    CardTasks = new List<CardTask>(),
                    Email = "",
                    ExplanatoryNotes = new List<ExplanatoryNote>(),
                    FirstName = "security",
                    SecondName = "",
                    ThirdName = "",
                    UserName = "security"
                };
                password = "security";

                result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Security.ToString());
                }

                user = new User()
                {
                    Birthday = new DateTime(),
                    CardEvents = new List<CardEvent>(),
                    CardTasks = new List<CardTask>(),
                    Email = "",
                    ExplanatoryNotes = new List<ExplanatoryNote>(),
                    FirstName = "hod",
                    SecondName = "",
                    ThirdName = "",
                    UserName = "hod"
                };
                password = "hod";

                result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.HeadOfDepartment.ToString());
                }

                user = new User()
                {
                    Birthday = new DateTime(),
                    CardEvents = new List<CardEvent>(),
                    CardTasks = new List<CardTask>(),
                    Email = "",
                    ExplanatoryNotes = new List<ExplanatoryNote>(),
                    FirstName = "hoo",
                    SecondName = "",
                    ThirdName = "",
                    UserName = "hoo"
                };
                password = "hoo";

                result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.HeadOfOrganisation.ToString());
                }
            }
            if (!dbContext.Departments.Any())
            {
                List<string> departments = new List<string>()
                {
                    "Главное здание",
                    "Территория Октябрьского поля",
                    "Общежитие 1",
                    "Общежитие 2",
                    "Общежитие 3",
                    "Общежитие 4"
                };
                foreach (var item in departments)
                {
                    dbContext.Departments.Add(new Department() { Name = item });
                }
            }
              dbContext.SaveChanges();
        }
    }
}
