using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Models
{
    public class CardTask
    {
        public int Id { get; set; }

        [Display(Name = "Наименование задачи")]
        [Required(ErrorMessage = "Не введено название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Не введено описание")]
        public string Desc { get; set; }

        [Display(Name="Комментарий исполнителя")]
        public string Answer { get; set; }

        [Display(Name="Объяснительная")]
        public ExplanatoryNote Explanation { get; set; }
        public int? ExplanationId { get; set; }

        [Display(Name = "Время начала")]
        [Required(ErrorMessage = "Не введено время начала")]
        public DateTime DateBegin { get; set; }

        [Display(Name = "Время конца")]
        [Required(ErrorMessage = "Не введено время конца")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Фактическое время отметки")]
        public DateTime RealDate { get; set; }

        [Display(Name = "Статус")]
        public bool State { get; set; }

        [Display(Name = "Отправитель")]
        public User User { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Отдел")]
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
