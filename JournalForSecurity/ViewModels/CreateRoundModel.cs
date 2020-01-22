using JournalForSecurity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class CreateRoundModel
    {
        [Display(Name = "Кол-во обходов")]
        [Required(ErrorMessage = "Не указано кол-во обходов")]
        [Range(1, 10, ErrorMessage = "Кол-во обходов в пределах от 1 до 10")]
        public int RoundCount { get; set; }

        [Display(Name = "Начало дня")]
        [Required(ErrorMessage = "Не указано время начала рабочего дня")]
        [DataType(DataType.Time)]
        public DateTime DayBegin { get; set; }

        [Display(Name = "Конец дня")]
        [Required(ErrorMessage = "Не указано время окончания рабочего дня")]
        [DataType(DataType.Time)]
        public DateTime DayEnd { get; set; }

        [Display(Name = "Отдел")]
        [Required(ErrorMessage = "Не указан отдел")]
        public string Department { get; set; }
    }
}
