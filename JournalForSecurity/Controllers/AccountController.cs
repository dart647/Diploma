using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalForSecurity.Data;
using JournalForSecurity.Models;
using JournalForSecurity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using JournalForSecurity.Service;

namespace JournalForSecurity.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<User> UserManager;
        readonly SignInManager<User> SignInManager;
        readonly AppDbContext dbContext;
        readonly DepartmentService departmentService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext dbContext)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this.dbContext = dbContext;
            this.departmentService = new DepartmentService(this.dbContext);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            ViewBag.Departments = await departmentService.GetDepartmentsNamesToSelectListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByNameAsync(model.Login);
                    user.Department = await dbContext.Departments.FirstOrDefaultAsync(d=>d.Name.Equals(model.Department));

                    await dbContext.SaveChangesAsync();

                    IEnumerable<string> roles = await UserManager.GetRolesAsync(user);
                    return RedirectToAction("Index", roles.FirstOrDefault());
                }
                else
                {
                    ModelState.AddModelError("", "Логин и(или) пароль введены не верно");
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            SelectList selectItem = await departmentService.GetDepartmentsNamesToSelectListAsync();
            ViewBag.Departments = selectItem;
            return View(model);
        }     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}