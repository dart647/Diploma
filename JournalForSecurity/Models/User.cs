﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime Birthday { get; set; }

        public List<CardRequest> CardRequests { get; set; }

        public List<CardEvent> CardEvents { get; set; }

        public List<CardTask> CardTasks { get; set; }

        public Department Department { get; set; }
        public int? DepartmentId { get; set; }

        public User()
        {
            CardEvents = new List<CardEvent>();
            CardRequests = new List<CardRequest>();
            CardTasks = new List<CardTask>();
        }
    }
}
