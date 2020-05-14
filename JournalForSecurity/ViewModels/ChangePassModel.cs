using JournalForSecurity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class ChangePassModel
    {
        [Display(Name = "Нынешний пароль")]
        [Required(ErrorMessage = "Укажите нынешний пароль")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "Новый пароль")]
        [Required(ErrorMessage = "Укажите новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Повторите новый пароль")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ComparePassword { get; set; }

        public string UserId { get; set; }
    }
}
