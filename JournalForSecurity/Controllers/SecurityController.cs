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
                .ThenInclude(jr => jr.Journal)
                .ThenInclude(en => en.Desc)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            ViewBag.UserFIO = String.Format($"{user.SecondName} {user.FirstName} {user.ThirdName}");

            SecJournalModel model = new SecJournalModel()
            {
                Journals = user.Department.Journal
                .Where(d => d.DateBegin.Date.Equals(DateTime.Now.Date))
                .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SecJournalModel model)
        {
            var str = await dbContext.Journals.FirstOrDefaultAsync(c => c.Id == model.JournalStrId);
            str.Status = true;
            str.RealDate = DateTime.Now;

            dbContext.SaveChanges();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> WriteExplanatoryNoteAsync(SecJournalModel model)
        {
            if (ModelState.IsValid)
            {
                var str = await dbContext.Journals.Include(n => n.Desc).FirstOrDefaultAsync(c => c.Id == model.JournalStrId);

                ExplanatoryNote note = new ExplanatoryNote()
                {
                    Explanation = model.Explanation,
                    TaskName = model.TaskName,
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name)).Id,
                    Date = DateTime.Now
                };

                await dbContext.AddAsync(note);
                await dbContext.SaveChangesAsync();

                str.Desc = note;
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Не удалось создать объяснительную");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Tasks()
        {
            var user = await dbContext.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            var tasks = await dbContext.CardTasks
                .Include(u => u.User)
                .Include(d => d.Department)
                .Where(c => c.Department == user.Department)
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
            var card = await dbContext.CardTasks.FirstOrDefaultAsync(c => c.Id == model.CardTaskId);
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