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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JournalForSecurity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        readonly UserManager<User> UserManager;
        readonly SignInManager<User> SignInManager;
        readonly RoleManager<IdentityRole> RoleManager;

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public IActionResult Index()
        {
            
            return View(UserManager.Users);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Roles = new SelectList(RoleManager.Roles);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.PhoneNumber.Length <= 11)
                {
                    User user = new User()
                    {
                        Email = model.Email,
                        Birthday = model.Birthday,
                        FirstName = model.FirstName,
                        SecondName = model.SecondName,
                        ThirdName = model.ThirdName,
                        PhoneNumber = model.PhoneNumber,
                        UserName = model.UserName
                    };

                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(await UserManager.FindByNameAsync(model.UserName), model.Role);
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Телефонный номер указан неверно");
                }
            }

            ViewBag.Roles = new SelectList(RoleManager.Roles);
            return View();
        }
    }
}