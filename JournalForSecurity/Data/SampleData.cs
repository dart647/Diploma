﻿using JournalForSecurity.Models;
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
            dbContext.SaveChanges();
        }
    }
}