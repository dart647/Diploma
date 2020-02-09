﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Models
{
    public class CardTask
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required]
        public string Desc { get; set; }

        [Display(Name = "Время начала")]
        [Required]
        public DateTime DateBegin { get; set; }

        [Display(Name = "Время конца")]
        [Required]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Статус")]
        public bool State { get; set; }

        [Display(Name = "Отправитель")]
        public User User { get; set; }
        public string UserId { get; set; }

        public Department Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
