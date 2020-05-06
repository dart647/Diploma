using JournalForSecurity.Models;
using JournalForSecurity.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class CreateRoundModel
    {
        [Display(Name = "Начало обхода")]
        [Required(ErrorMessage = "Не указано время начала рабочего дня")]
        [DataType(DataType.Time, ErrorMessage = "Введено некоректное время")]
        public DateTime DayBegin { get; set; }

        [Display(Name = "Конец обхода")]
        [Required(ErrorMessage = "Не указано время окончания рабочего дня")]
        [DataType(DataType.Time, ErrorMessage = "Введено некоректное время")]
        public DateTime DayEnd { get; set; }

        [Display(Name = "Отдел")]
        [Required(ErrorMessage = "Не указан отдел")]
        public string Department { get; set; }

        public List<Journal> Rounds {get; set;}
    }
}
