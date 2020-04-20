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

        public async Task<IActionResult> Index(string department)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));

            ViewBag.UserFIO = String.Format($"{user.SecondName} {user.FirstName} {user.ThirdName}");

            SecJournalModel model = new SecJournalModel()
            {
                Journals = dbContext.Journals
                .Where(d => d.DateBegin.Date.Equals(DateTime.Now.Date) && d.Department.Name.Equals(department))
                .ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Tasks(string department)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));

            ViewBag.UserFIO = String.Format($"{user.SecondName} {user.FirstName} {user.ThirdName}");

            var tasks = await dbContext.CardTasks
                .Include(u => u.User)
                .Include(d => d.Department)
                .Include(e => e.Explanation)
                .Where(c => c.Department.Equals(department))
                .ToListAsync();

            var model = new SecTaskModel()
            {
                cardTasks = tasks
            };

            return View(model);
        }

        public async Task<IActionResult> EventsAsync(string department)
        {
            List<CardEvent> items = new List<CardEvent>();
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            items = await dbContext.CardEvents
                .Include(d => d.Department)
                .Include(u => u.User)
                .Where(c => c.Department.Equals(department))
                .ToListAsync();

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SecJournalModel model)
        {
            if (ModelState.IsValid)
            {
                var str = await dbContext.Journals.FirstOrDefaultAsync(c => c.Id == model.JournalStrId);
                str.Status = true;
                str.RealDate = DateTime.Now;
                str.Comment = model.Comment;

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Не удалось подтвердить");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> WriteExplanatoryNoteToJournalAsync(SecJournalModel model)
        {
            if (ModelState.IsValid)
            {
                var str = await dbContext.Journals.Include(n => n.Explanation).FirstOrDefaultAsync(c => c.Id == model.JournalStrId);

                ExplanatoryNote note = new ExplanatoryNote()
                {
                    Explanation = model.Explanation,
                    TaskName = model.TaskName,
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name)).Id,
                    Date = DateTime.Now
                };

                await dbContext.AddAsync(note);
                await dbContext.SaveChangesAsync();

                str.Explanation = note;
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Не удалось создать объяснительную");
            }
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> WriteExplanatoryNoteToTaskAsync(SecTaskModel model)
        {
            if (ModelState.IsValid)
            {
                var card = await dbContext.CardTasks.Include(n => n.Explanation).FirstOrDefaultAsync(c => c.Id == model.CardTaskId);

                ExplanatoryNote note = new ExplanatoryNote()
                {
                    Explanation = model.Explanation,
                    TaskName = model.TaskName,
                    UserId = dbContext.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name)).Id,
                    Date = DateTime.Now
                };

                await dbContext.AddAsync(note);
                await dbContext.SaveChangesAsync();

                card.Explanation = note;
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Tasks");
            }
            else
            {
                ModelState.AddModelError("", "Не удалось создать объяснительную");
            }
            return View("Tasks", model);
        }

        [HttpPost]
        public async Task<IActionResult> Tasks(SecTaskModel model)
        {
            if (ModelState.IsValid)
            {
                var card = await dbContext.CardTasks.FirstOrDefaultAsync(c => c.Id == model.CardTaskId);
                card.Answer = model.Answer;
                card.State = true;

                dbContext.SaveChanges();

                return RedirectToAction("Tasks");
            }
            else
            {
                ModelState.AddModelError("", "Не удалось подтвердить");
            }

            return View(model);
        }

    }
}