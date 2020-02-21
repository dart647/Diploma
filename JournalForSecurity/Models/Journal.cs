using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Models
{
    public class Journal
    {
        public int Id { get; set; }

        [Display(Name = "Время начала")]
        public DateTime DateBegin { get; set; }

        [Display(Name = "Время конца")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Реальное время")]
        public DateTime RealDate { get; set; }

        [Display(Name = "Статус")]
        public bool Status { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        [Display(Name = "Объяснительная")]
        public ExplanatoryNote Explanation { get; set; }

        [Display(Name = "Отдел")]
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
