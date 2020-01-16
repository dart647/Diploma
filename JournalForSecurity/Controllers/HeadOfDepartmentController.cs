using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JournalForSecurity.Controllers
{
    [Authorize(Roles = "HeadOfDepartment")]
    public class HeadOfDepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}