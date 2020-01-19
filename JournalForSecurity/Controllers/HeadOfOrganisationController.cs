using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalForSecurity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tasks()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Requests()
        {
            return View();
        }
    }
}