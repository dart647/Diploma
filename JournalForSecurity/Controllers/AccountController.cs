using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalForSecurity.Data;
using JournalForSecurity.Models;
using JournalForSecurity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        readonly RoleManager<IdentityRole> RoleManager;
        readonly AppDbContext dbContext;
        readonly DepartmentService departmentService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
            this.dbContext = dbContext;
            this.departmentService = new DepartmentService(this.dbContext);
        }

        public IActionResult Login()
        {
            //ViewBag.Departments = await departmentService.GetDepartmentsNamesToSelectListAsync();
            return View();
        }

        public IActionResult SwitchDepartment(string role)
        {
            ViewBag.Role = role;
            return View(dbContext.Departments.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Login);
                if (user.isDismissed)
                {
                    ModelState.AddModelError("", "Вы были уволены, вход невозможен");
                    return View(model);
                }
                var result = await SignInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
                if (result.Succeeded)
                {
                    IEnumerable<string> roles = await UserManager.GetRolesAsync(user);
                    if (await UserManager.IsInRoleAsync(user, Roles.Security.ToString()))
                    {
                        return RedirectToAction("SwitchDepartment", new { role = Roles.Security.ToString() });
                    }
                    if (await UserManager.IsInRoleAsync(user, Roles.HeadOfDepartment.ToString()))
                    {
                        return RedirectToAction("SwitchDepartment", new { role = Roles.HeadOfDepartment.ToString() });
                    }
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