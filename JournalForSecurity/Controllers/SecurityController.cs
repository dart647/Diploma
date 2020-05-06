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
using Microsoft.AspNetCore.Routing;
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
            ViewBag.Department = RouteData.Values["department"];

            SecJournalModel model = new SecJournalModel()
            {
                Journals = dbContext.Journals
                .Include(j => j.Explanation)
                .Where(d => d.DateBegin.Date.Equals(DateTime.Now.Date) && d.Department.Name.Equals(department))
                .ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Tasks()
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));

            ViewBag.UserFIO = String.Format($"{user.SecondName} {user.FirstName} {user.ThirdName}");
            ViewBag.Department = RouteData.Values["department"];

            var tasks = await dbContext.CardTasks
                .Include(u => u.User)
                .Include(d => d.Department)
                .Include(e => e.Explanation)
                .Where(c => c.Department.Name.Equals(RouteData.Values["department"].ToString()))
                .ToListAsync();

            var model = new SecTaskModel()
            {
                cardTasks = tasks
            };

            return View(model);
        }

        public async Task<IActionResult> EventsAsync()
        {
            List<CardEvent> items = new List<CardEvent>();
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            ViewBag.Department = RouteData.Values["department"];

            items = await dbContext.CardEvents
                .Include(d => d.Department)
                .Include(u => u.User)
                .Where(c => c.Department.Name.Equals(RouteData.Values["department"].ToString()))
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

                var url = RedirectToAction("Index", "Security");
                url.RouteValues = new RouteValueDictionary();
                var result = url.RouteValues.TryAdd("department", RouteData.Values["department"]);
                if (!result)
                {
                    ModelState.AddModelError("", "Ошибка переадресовки");
                    ViewBag.Department = RouteData.Values["department"];
                    return View("Index", model);
                }

                return url;
            }
            else
            {
                ModelState.AddModelError("", "Не удалось подтвердить");
                ViewBag.Department = RouteData.Values["department"];
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
                    UserId = dbContext.Users.Where(u => u.UserName.Equals(User.Identity.Name)).Select(u => u.Id).FirstOrDefault(),
                    Date = DateTime.Now,
                    TaskType = ENTaskType.JournalStr,
                    DepartmentId = dbContext.Departments.Where(d => d.Name.Equals(RouteData.Values["department"].ToString())).Select(d => d.Id).FirstOrDefault()
                };

                await dbContext.AddAsync(note);
                await dbContext.SaveChangesAsync();

                str.ExplanationId = dbContext.ExplanatoryNotes.Where(e => e.TaskName.Equals(note.TaskName)).Select(e => e.Id).FirstOrDefault();
                await dbContext.SaveChangesAsync();

                var url = RedirectToAction("Index", "Security");
                url.RouteValues = new RouteValueDictionary();
                var result = url.RouteValues.TryAdd("department", RouteData.Values["department"]);
                if (!result)
                {
                    ModelState.AddModelError("", "Ошибка переадресовки");
                    ViewBag.Department = RouteData.Values["department"];
                    return View("Index", model);
                }

                return url;
            }
            else
            {
                ModelState.AddModelError("", "Не удалось создать объяснительную");
                ViewBag.Department = RouteData.Values["department"];
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
                    UserId = dbContext.Users.Where(u => u.UserName.Equals(User.Identity.Name)).Select(u => u.Id).FirstOrDefault(),
                    Date = DateTime.Now,
                    TaskType = ENTaskType.Task,
                    DepartmentId = dbContext.Departments.Where(d => d.Name.Equals(RouteData.Values["department"])).Select(d => d.Id).FirstOrDefault()
                };

                await dbContext.AddAsync(note);
                await dbContext.SaveChangesAsync();

                card.ExplanationId = dbContext.ExplanatoryNotes.Where(e => e.TaskName.Equals(note.TaskName)).Select(e => e.Id).FirstOrDefault();
                await dbContext.SaveChangesAsync();

                var url = RedirectToAction("Tasks", "Security");
                url.RouteValues = new RouteValueDictionary();
                var result = url.RouteValues.TryAdd("department", RouteData.Values["department"]);
                if (!result)
                {
                    ModelState.AddModelError("", "Ошибка переадресовки");
                    ViewBag.Department = RouteData.Values["department"];
                    return View("Tasks", model);
                }

                return url;
            }
            else
            {
                ModelState.AddModelError("", "Не удалось создать объяснительную");
                ViewBag.Department = RouteData.Values["department"];
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

                var url = RedirectToAction("Tasks", "Security");
                url.RouteValues = new RouteValueDictionary();
                var result = url.RouteValues.TryAdd("department", RouteData.Values["department"]);
                if (!result)
                {
                    ModelState.AddModelError("", "Ошибка переадресовки");
                    ViewBag.Department = RouteData.Values["department"];
                    return View("Tasks", model);
                }

                return url;
            }
            else
            {
                ModelState.AddModelError("", "Не удалось подтвердить");
                ViewBag.Department = RouteData.Values["department"];
            }

            return View(model);
        }

    }
}