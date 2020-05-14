using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string ThirdName { get; set; }

        [Display(Name = "День рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Некоректная дата")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public override string Email { get => base.Email; set => base.Email = value; }

        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11, ErrorMessage = "Телефон должен содержать 11 цифр", MinimumLength = 11)]
        [RegularExpression(@"8[0-9]{10}", ErrorMessage = "телефон должен соответствовать шаблону 8xxxxxxxxxx")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        public bool isDismissed { get; set; }

        public List<CardEvent> CardEvents { get; set; }

        public List<CardTask> CardTasks { get; set; }

        public List<ExplanatoryNote> ExplanatoryNotes { get; set; }

        public User()
        {
            CardEvents = new List<CardEvent>();
            CardTasks = new List<CardTask>();
            ExplanatoryNotes = new List<ExplanatoryNote>();
        }


    }

    public enum Roles
    {
        Admin,
        HeadOfDepartment,
        HeadOfOrganisation,
        Security
    }
}
