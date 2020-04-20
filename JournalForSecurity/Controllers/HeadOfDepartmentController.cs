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
                .Where(j => j.DateBegin.Date.Equals(DateTime.Now.Date) && j.Department.Name.Equals(RouteData.Values["department"].ToString()))
                .ToListAsync();

            SecJournalModel model = new SecJournalModel()
            {
                Journals = journalStr
            };

            return View(model);
        }

        public async Task<IActionResult> TasksAsync(string department)
        {

            var tasks = await dbContext.CardTasks
                .Include(u => u.User)
                .Include(d => d.Department)
                .Include(e=>e.Explanation)
                .ThenInclude(u=>u.User)
                .Where(c => c.Department.Name.Equals(department))
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

            items = await dbContext.CardEvents
                .Include(d => d.Department)
                .Include(u => u.User)
                .Where(c => c.Department.Name.Equals(department))
                .ToListAsync();

            return View(items);
        }

        public async Task<IActionResult> RequestsAsync(string department)
        {
            List<CardRequest> items = new List<CardRequest>();

            items = await dbContext.CardRequests
                .Include(d => d.Department)
                .Include(u => u.User)
                .Where(c => c.Department.Name.Equals(department))
                .ToListAsync();

            return View(items);
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        public IActionResult CreateRound(string department)
        {
            ViewBag.Department = department;
            return View();
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
                    Department = task.Department,
                    Desc = task.Desc,
                    Name = task.Name,
                    State = false,
                    User = task.User
                };

                await dbContext.CardTasks.AddAsync(newCard);
                dbContext.SaveChanges();

                return RedirectToAction("Tasks");
            }
            else
            {
                ModelState.AddModelError("", "Ошибка ввода данных");
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
                double betweenRounds = (double) model.DayEnd.Subtract(model.DayBegin).Hours / model.RoundCount;
                int timeToRound = 1;
                Department department = await dbContext.Departments.FirstOrDefaultAsync(d => d.Name.Equals(model.Department));

                for (int i = 0; i < model.RoundCount; i++)
                {
                    Journal journalStr = new Journal()
                    {
                        DateBegin = DateSetter.SetToday().AddHours(model.DayBegin.AddHours(betweenRounds * i).Hour),
                        DateEnd = DateSetter.SetToday().AddHours(model.DayBegin.AddHours(timeToRound + betweenRounds * i).Hour),
                        DepartmentId = department.Id,
                        Status = false
                    };

                    await dbContext.Journals.AddAsync(journalStr);
                    dbContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Введены некорректные данные");
            }
            ViewBag.Departments = await departmentService.GetDepartmentsNamesToSelectListAsync();
            return View(model);
        }
    }
}