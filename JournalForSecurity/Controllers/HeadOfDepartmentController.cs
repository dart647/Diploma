using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalForSecurity.Data;
using JournalForSecurity.Models;
using JournalForSecurity.Service;
using JournalForSecurity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace JournalForSecurity.Controllers
{
    [Authorize(Roles = "HeadOfDepartment")]
    public class HeadOfDepartmentController : Controller
    {
        AppDbContext dbContext;
        DepartmentService departmentService;

        public HeadOfDepartmentController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.departmentService = new DepartmentService(this.dbContext);
        }

        public async Task<IActionResult> IndexAsync()
        {
            var journalStr = await dbContext.Journals
                .Include(d => d.Department)
                .Include(e => e.Explanation)
                .ThenInclude(u => u.User)
                .Where(j => j.DateBegin.Date.Equals(DateTime.Now.Date) && j.Department.Name.Equals(RouteData.Values["department"].ToString()))
                .ToListAsync();

            ViewBag.Department = RouteData.Values["department"];

            SecJournalModel model = new SecJournalModel()
            {
                Journals = journalStr
            };

            return View(model);
        }

        public async Task<IActionResult> TasksAsync()
        {

            var tasks = await dbContext.CardTasks
                .Include(u => u.User)
                .Include(d => d.Department)
                .Include(e => e.Explanation)
                .ThenInclude(u => u.User)
                .Where(c => c.Department.Name.Equals(RouteData.Values["department"].ToString()))
                .ToListAsync();

            ViewBag.Department = RouteData.Values["department"];

            var model = new SecTaskModel()
            {
                cardTasks = tasks
            };

            return View(model);
        }

        public async Task<IActionResult> EventsAsync()
        {
            List<CardEvent> items = new List<CardEvent>();

            items = await dbContext.CardEvents
                .Include(d => d.Department)
                .Include(u => u.User)
                .Where(c => c.Department.Name.Equals(RouteData.Values["department"].ToString()))
                .ToListAsync();

            ViewBag.Department = RouteData.Values["department"];

            return View(items);
        }

        public IActionResult CreateTask()
        {
            ViewBag.Department = RouteData.Values["department"];
            return View();
        }

        public async Task<IActionResult> CreateRoundAsync()
        {
            var department = RouteData.Values["department"];
            ViewBag.Department = department;

            var model = new CreateRoundModel()
            {
                Rounds = await dbContext.Journals
                .Include(d => d.Department)
                .Where(j => j.DateBegin.Date.Equals(DateTime.Now.Date) &&
                j.Department.Name.Equals(department.ToString()))
                .ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync(CardTask task)
        {
            if (ModelState.IsValid)
            {
                CardTask newCard = new CardTask()
                {
                    DateBegin = task.DateBegin,
                    DateEnd = task.DateEnd,
                    DepartmentId = dbContext.Departments.Where(d => d.Name.Equals(RouteData.Values["department"].ToString())).Select(d => d.Id).FirstOrDefault(),
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

        //[HttpPost]
        //public async Task<IActionResult> EditCommentAsync(String comment)
        //{
        //    if(!String.IsNullOrWhiteSpace(comment) || !String.IsNullOrEmpty(comment))
        //    {
        //        //ДОДЕЛАТЬ
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<IActionResult> CreateRoundAsync(CreateRoundModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Department = RouteData.Values["department"];
                if (model.DayEnd <= model.DayBegin)
                {
                    ModelState.AddModelError("", "Время окончания обхода не может быть меньше или равным времени начала обхода");
                    return View(model);
                }

                Department department = await dbContext.Departments.FirstOrDefaultAsync(d => d.Name.Equals(model.Department));

                if (dbContext.Journals.Any(j => j.DateBegin.Equals(model.DayBegin) 
                    && j.DateEnd.Equals(model.DayEnd) 
                    && j.DepartmentId.Equals(department.Id)))
                {
                    ModelState.AddModelError("", "Обход с таким временем начала и окончания уже существует");
                    return View(model);
                }

                Journal journalStr = new Journal()
                {
                    DateBegin = DateTime.Now.Date.AddHours(model.DayBegin.Hour).AddMinutes(model.DayBegin.Minute),
                    DateEnd = DateTime.Now.Date.AddHours(model.DayEnd.Hour).AddMinutes(model.DayEnd.Minute),
                    DepartmentId = department.Id,
                    Status = false
                };

                await dbContext.Journals.AddAsync(journalStr);
                dbContext.SaveChanges();

                model.Rounds = await dbContext.Journals
                    .Include(d => d.Department)
                    .Where(j => j.DateBegin.Date.Equals(DateTime.Now.Date) &&
                    j.Department.Id.Equals(department.Id))
                    .ToListAsync();

                var url = RedirectToAction("CreateRound");
                url.RouteValues = new RouteValueDictionary();
                var result = url.RouteValues.TryAdd("department", RouteData.Values["department"]);
                if (!result)
                {
                    ModelState.AddModelError("", "Ошибка переадресовки");
                    ViewBag.Department = RouteData.Values["department"];
                    return View(model);
                }
                return url;
            }
            else
            {
                ModelState.AddModelError("", "Введены некорректные данные");
                ViewBag.Department = RouteData.Values["department"];
            }
            return View(model);
        }
    }
}