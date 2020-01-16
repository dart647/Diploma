using JournalForSecurity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указано Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана Фамилия")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Не указано Отчество")]
        public string ThirdName { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указана Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Не указан Телефон")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не указан Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указана Роль")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Не указан Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    } 
}
