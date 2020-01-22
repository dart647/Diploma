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
            var user = await dbContext.Users
                .Include(u => u.Department)
                .ThenInclude(j => j.JournalRows)
                .ThenInclude(jr => jr.Journal)
                .ThenInclude(en=>en.Desc)
                .ThenInclude(u=>u.User)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            ViewBag.DepartmentName = user.Department.Name;

            SecJournalModel model = new SecJournalModel()
            {
                Journals = user.Department.JournalRows.Select(d => d.Journal).Where(d=>d.DateBegin.Date.Equals(DateTime.Now.Date)).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> TasksAsync()
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

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Requests()
        {
            return View();
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        public async Task<IActionResult> CreateRoundAsync()
        {
            ViewBag.Departments = await departmentService.GetDepartmentsNamesToSelectListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoundAsync(CreateRoundModel model)
        {
            if (ModelState.IsValid)
            {
                int betweenRounds = model.DayEnd.Subtract(model.DayBegin).Hours / model.RoundCount;
                int timeToRound = 1;
                Department department = await dbContext.Departments.FirstOrDefaultAsync(d => d.Name.Equals(model.Department));

                for (int i = 0; i < model.RoundCount; i++)
                {
                    Journal journalStr = new Journal()
                    {
                        DateBegin = DateSetter.SetToday().AddHours(model.DayBegin.AddHours(betweenRounds * i).Hour),
                        DateEnd = DateSetter.SetToday().AddHours(model.DayBegin.AddHours(timeToRound + betweenRounds * i).Hour),
                        Status = false
                    };

                    await dbContext.Journals.AddAsync(journalStr);
                    dbContext.SaveChanges();

                    journalStr.JournalRows.Add(new JournalRow() { JournalId = journalStr.Id, DepartmentId = department.Id });
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