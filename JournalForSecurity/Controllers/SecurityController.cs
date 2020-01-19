using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalForSecurity.Data;
using JournalForSecurity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JournalForSecurity.Controllers
{
    [Authorize(Roles = "Security")]
    public class SecurityController : Controller
    {
        AppDbContext dbContext;
        readonly UserManager<User> UserManager;

        public SecurityController(AppDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            UserManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            ViewData["DepartmentName"] = user.Department.Name;
            return View(user.Department.JournalRows.Select(j=>j.Journal).ToList());
        }

        public IActionResult Tasks()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

    }
}