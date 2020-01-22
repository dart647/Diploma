using JournalForSecurity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Service
{
    public class DepartmentService
    {
        AppDbContext dbContext;

        public DepartmentService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<SelectList> GetDepartmentsNamesToSelectListAsync()
        {
            return new SelectList(await dbContext.Departments
                .Select(d => d.Name)
                .ToListAsync());
        }
    }
}
