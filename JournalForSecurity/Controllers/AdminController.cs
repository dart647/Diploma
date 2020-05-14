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

        public async Task<IActionResult> IndexAsync(bool isDismissed = false)
        {
            var model = new UsersModel();
            if (!isDismissed)
            {
                model.Users = await UserManager.Users.Where(u => !u.isDismissed).ToListAsync();
            }
            else
            {
                model.Users = await UserManager.Users.Where(u => u.isDismissed).ToListAsync();
            }

            return View(model);
        }

        public IActionResult Register()
        {
            ViewBag.Roles = new SelectList(RoleManager.Roles);
            return View();
        }

        public async Task<IActionResult> EditUserAsync(string id)
        {            
            return View(await UserManager.FindByIdAsync(id));
        }

        public async Task<IActionResult> EditPasswordAsync(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var model = new ChangePassModel
            {
                UserId = user.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(UsersModel model)
        {
            var user = await UserManager.FindByIdAsync(model.dUserId);
            if (user == null)
            {
                ModelState.AddModelError("", "Ошибка удаления пользователя: пользователь не найден");
                return View(model);
            }

            user.isDismissed = !user.isDismissed;
            var result = await UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Ошибка удаления пользователя: невозможно обновление базы данных");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditUserAsync(User user)
        {
            if (ModelState.IsValid)
            {
                var nUser = await UserManager.FindByIdAsync(user.Id);
                nUser.FirstName = user.FirstName;
                nUser.SecondName = user.SecondName;
                nUser.ThirdName = user.ThirdName;
                nUser.Birthday = user.Birthday;
                nUser.Email = user.Email;
                nUser.PhoneNumber = user.PhoneNumber;

                var result = await UserManager.UpdateAsync(nUser);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Ошибка редактирования пользователя: невозможно обновление базы данных");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Ошибка валидации данных. Некоректные данные!");
                return View(user);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditPasswordAsync(ChangePassModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.UserId);

                var result = await UserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Ошибка смены пароля");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Ошибка валидации данных. Некоректные данные!");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
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