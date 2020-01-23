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
        public static void Initialize(AppDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.AddRange(
                    new IdentityRole("Admin") { NormalizedName = "ADMIN" },
                    new IdentityRole("Security") { NormalizedName = "SECURITY" },
                    new IdentityRole("HeadOfDepartment") { NormalizedName = "HEADOFDEPARTMENT" },
                    new IdentityRole("HeadOfOrganisation") { NormalizedName = "HEADOFORGANISATION" }
                    );
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
            if (!dbContext.Journals.Any())
            {
                foreach (var item in dbContext.Departments.ToList())
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Journal journal = new Journal()
                        {
                            DateBegin = new DateTime(2020, 1, 20, 8 + i * 2, 0, 0),
                            DateEnd = new DateTime(2020, 1, 20, 9 + i * 2, 0, 0),
                            DepartmentId = item.Id
                        };
                        dbContext.Journals.Add(journal);

                        dbContext.SaveChanges();
                    }
                }
            }
            if (!dbContext.CardTasks.Any())
            {
                foreach (var journal in dbContext.Journals.ToList())
                {
                    CardTask card = new CardTask()
                    {
                        DateBegin = journal.DateBegin,
                        DateEnd = journal.DateEnd,
                        Name = "Задача " + journal.DateBegin,
                        State = false,
                        UserId = dbContext.Users.FirstOrDefault(u => u.UserName == "hod").Id
                    };
                    dbContext.CardTasks.Add(card);

                    dbContext.SaveChanges();
                }
            }
            dbContext.SaveChanges();
        }
    }
}
