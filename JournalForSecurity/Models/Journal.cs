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

        [Display(Name = "Описание")]
        public string Desc { get; set; }

        [Display(Name = "Отдел")]
        public List<JournalRow> JournalRows { get; set; }

        public Journal()
        {
            JournalRows = new List<JournalRow>();
        }
    }
}
