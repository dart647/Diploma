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

        public IActionResult Index(string mode, int page = 1)
        {
            ViewBag.Mode = mode;

            var report = new ReportModel(dbContext);
            var model = report.GetReport(mode, page);

            return View(model);
        }

        public async Task<IActionResult> EventsAsync()
        {
            List<CardEvent> items = new List<CardEvent>();

            items = await dbContext.CardEvents
                .Include(d => d.Department)
                .Include(u => u.User)
                .ToListAsync();

            return View(items);
        }

        public async Task<IActionResult> CreateEventAsync()
        {
            ViewBag.departments = new SelectList(await dbContext.Departments.Select(d => d.Name).ToListAsync());
            return View();
        }

        public IActionResult Statistic(string departmentName)
        {
            var department = dbContext.Departments.FirstOrDefault(d => d.Name.Equals(departmentName));
            var model = new StatisticModel(dbContext);
            model = model.GetDepartmentStatistic(department);
            return View(model);
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
    }
}