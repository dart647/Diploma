using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalForSecurity.Data;
using JournalForSecurity.Models;
using JournalForSecurity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace JournalForSecurity.Controllers
{
    [Authorize(Roles = "HeadOfOrganisation")]
    public class HeadOfOrganisationController : Controller
    {
        AppDbContext dbContext;

        public HeadOfOrganisationController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index(string mode = "journal", int page = 1)
        {
            ViewBag.Mode = mode;

            var report = new ReportModel(dbContext);
            var model = report.GetReport(mode, page);

            return View(model);
        }

        public async Task<IActionResult> EventsAsync()
        {
            var items = await dbContext.CardEvents
                .Include(d => d.Department)
                .Include(u => u.User)
                .ToListAsync();

            foreach (var item in items)
            {
                if (item.AlertResult != null)
                {
                    item.Alerts = item.AlertResult.Split("\n").ToList();
                }
            }

            return View(items);
        }

        public async Task<IActionResult> TasksAsync()
        {
            var tasks = await dbContext.CardTasks
                .Include(u => u.User)
                .Include(d => d.Department)
                .Include(e => e.Explanation)
                .ThenInclude(u => u.User)
                .Where(t => t.DateEnd.AddDays(1).Date >= DateTime.Now.Date)
                .ToListAsync();

            var model = new SecTaskModel()
            {
                cardTasks = tasks
            };

            return View(model);
        }

        public IActionResult CreateTask()
        {
            ViewBag.Departments = new SelectList(dbContext.Departments.Select(d => d.Name));
            return View();
        }

        public async Task<IActionResult> CreateEventAsync()
        {
            ViewBag.departments = new SelectList(await dbContext.Departments.Select(d => d.Name).ToListAsync());
            return View();
        }

        public async Task<IActionResult> EditTaskAsync(int id)
        {
            var task = await dbContext.CardTasks.FirstOrDefaultAsync(t => t.Id.Equals(id));
            return View(task);
        }

        public async Task<IActionResult> EditEventAsync(int id)
        {
            ViewBag.departments = new SelectList(await dbContext.Departments.Select(d => d.Name).ToListAsync());
            var uEvent = await dbContext.CardEvents.FirstOrDefaultAsync(t => t.Id.Equals(id));
            return View(uEvent);
        }

        public IActionResult Statistic(string departmentName)
        {
            var department = dbContext.Departments.FirstOrDefault(d => d.Name.Equals(departmentName));
            var model = new StatisticModel(dbContext);
            model = model.GetDepartmentStatistic(department);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync(CardTask task, string department)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(dbContext.Departments.Select(d => d.Name));
                if (task.DateEnd <= task.DateBegin)
                {
                    ModelState.AddModelError("", "Время окончания обхода не может быть меньше или равным времени начала обхода");
                    return View(task);
                }
                CardTask newCard = new CardTask()
                {
                    DateBegin = task.DateBegin,
                    DateEnd = task.DateEnd,
                    DepartmentId = await dbContext.Departments.Where(d => d.Name.Equals(department)).Select(d => d.Id).FirstOrDefaultAsync(),
                    Desc = task.Desc,
                    Name = task.Name,
                    State = false,
                    UserId = dbContext.Users.Where(u => u.UserName.Equals(User.Identity.Name)).Select(u => u.Id).FirstOrDefault()
                };

                await dbContext.CardTasks.AddAsync(newCard);
                dbContext.SaveChanges();

                var url = RedirectToAction("Tasks");
                url.RouteValues = new RouteValueDictionary();
                var result = url.RouteValues.TryAdd("department", RouteData.Values["department"]);
                if (!result)
                {
                    ModelState.AddModelError("", "Ошибка переадресовки");
                    ViewBag.Department = RouteData.Values["department"];
                    return View();
                }

                return url;
            }
            else
            {
                ModelState.AddModelError("", "Ошибка ввода данных");
                ViewBag.Department = RouteData.Values["department"];
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync(CardEvent card, string department)
        {
            if (ModelState.IsValid)
            {
                User user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                Department depart = await dbContext.Departments.FirstOrDefaultAsync(d => d.Name.Equals(department));

                CardEvent newCard = new CardEvent()
                {
                    Department = depart,
                    Date = DateTime.Now,
                    Desc = card.Desc,
                    IsAlertResult = false,
                    Name = card.Name,
                    User = user
                };

                await dbContext.CardEvents.AddAsync(newCard);
                dbContext.SaveChanges();

                return RedirectToAction("Events");
            }
            else
            {
                ModelState.AddModelError("", "Ошибка ввода данных");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditTaskAsync(CardTask task)
        {
            if (ModelState.IsValid)
            {
                if (task.DateEnd <= task.DateBegin)
                {
                    ModelState.AddModelError("", "Время окончания обхода не может быть меньше или равным времени начала обхода");
                    return View(task);
                }
                var uTask = await dbContext.CardTasks.FirstOrDefaultAsync(c => c.Id.Equals(task.Id));

                uTask.Desc = task.Desc;
                uTask.Name = task.Name;

                if (uTask.DateBegin > DateTime.Now)
                {
                    uTask.DateBegin = task.DateBegin;
                    uTask.DateEnd = task.DateEnd;
                }

                dbContext.CardTasks.Update(uTask);
                await dbContext.SaveChangesAsync();

                var url = RedirectToAction("Tasks");
                url.RouteValues = new RouteValueDictionary();
                var result = url.RouteValues.TryAdd("department", RouteData.Values["department"]);
                if (!result)
                {
                    ModelState.AddModelError("", "Ошибка переадресовки");
                    ViewBag.Department = RouteData.Values["department"];
                    return View(task);
                }
                return url;
            }
            else
            {
                ModelState.AddModelError("", "Введены некорректные данные");
                ViewBag.Department = RouteData.Values["department"];
            }
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> EditEventAsync(CardEvent newEvent, string department)
        {
            if (ModelState.IsValid)
            {
                var uEvent = await dbContext.CardEvents.FirstOrDefaultAsync(c => c.Id.Equals(newEvent.Id));
                Department depart = await dbContext.Departments.FirstOrDefaultAsync(d => d.Name.Equals(department));

                uEvent.Desc = newEvent.Desc;
                uEvent.Name = newEvent.Name;
                uEvent.Department = depart;

                dbContext.CardEvents.Update(uEvent);
                await dbContext.SaveChangesAsync();

                var url = RedirectToAction("Events");
                url.RouteValues = new RouteValueDictionary();
                var result = url.RouteValues.TryAdd("department", RouteData.Values["department"]);
                if (!result)
                {
                    ModelState.AddModelError("", "Ошибка переадресовки");
                    ViewBag.Department = RouteData.Values["department"];
                    return View(newEvent);
                }
                return url;
            }
            else
            {
                ModelState.AddModelError("", "Введены некорректные данные");
                ViewBag.Department = RouteData.Values["department"];
            }
            return View(newEvent);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEventAsync(int id)
        {
            var cEvent = await dbContext.CardEvents.FirstOrDefaultAsync(e => e.Id == id);

            dbContext.CardEvents.Remove(cEvent);
            dbContext.SaveChanges();

            var url = RedirectToAction("Events");
            url.RouteValues = new RouteValueDictionary();
            var result = url.RouteValues.TryAdd("department", RouteData.Values["department"]);
            if (!result)
            {
                ModelState.AddModelError("", "Ошибка переадресовки");
                ViewBag.Department = RouteData.Values["department"];
                return View("Events");
            }
            return url;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTaskAsync(int id)
        {
            var cTask = await dbContext.CardTasks.FirstOrDefaultAsync(e => e.Id == id);

            dbContext.CardTasks.Remove(cTask);
            dbContext.SaveChanges();

            var url = RedirectToAction("Tasks");
            url.RouteValues = new RouteValueDictionary();
            var result = url.RouteValues.TryAdd("department", RouteData.Values["department"]);
            if (!result)
            {
                ModelState.AddModelError("", "Ошибка переадресовки");
                ViewBag.Department = RouteData.Values["department"];
                return View("Tasks");
            }
            return url;
        }
    }
}