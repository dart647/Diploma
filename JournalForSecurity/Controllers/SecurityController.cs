using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalForSecurity.Data;
using JournalForSecurity.Models;
using JournalForSecurity.ViewModels;
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

        public SecurityController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = await dbContext.Users
                .Include(u => u.Department)
                .ThenInclude(j => j.JournalRows)
                .ThenInclude(jr => jr.Journal)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            ViewBag.DepartmentName = user.Department.Name;

            SecJournalModel model = new SecJournalModel()
            {
                Journals = user.Department.JournalRows.Select(d => d.Journal).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SecJournalModel model)
        {
            var str = await dbContext.Journals.FirstOrDefaultAsync(c => c.Id == model.JournalStrId);
            str.Status = true;

            dbContext.SaveChanges();

            return View(model);
        }


        public async Task<IActionResult> Tasks()
        {
            var user = await dbContext.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            var tasks = await dbContext.CardTasks
                .Include(u=>u.User)
                .Include(d => d.Department)
                .Where(c=>c.Department == user.Department)
                .ToListAsync();

            var model = new SecTaskModel()
            {
                cardTasks = tasks
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Tasks(SecTaskModel model)
        {
            var card = await dbContext.CardTasks.FirstOrDefaultAsync(c=>c.Id == model.CardTaskId);
            card.State = true;

            dbContext.SaveChanges();

            return View(model);
        }

        public IActionResult Events()
        {
            return View();
        }

    }
}