using JournalForSecurity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class SecJournalModel
    {
        public List<Journal> Journals { get; set; }

        public int JournalStrId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Объяснение")]
        public string Explanation { get; set; }

        [Display(Name = "Наименование задачи")]
        public string TaskName { get; set; }

        [Required]
        [Display(Name ="Комментарий")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public SecJournalModel()
        {
            Journals = new List<Journal>();
        }
    }
}
