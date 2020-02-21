using JournalForSecurity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class SecTaskModel
    {
        public List<CardTask> cardTasks { get; set; }

        public int CardTaskId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Объяснение")]
        public string Explanation { get; set; }

        [Display(Name = "Наименование задачи")]
        public string TaskName { get; set; }

        [Required]
        [Display(Name ="Ответ")]
        [DataType(DataType.MultilineText)]
        public string Answer { get; set; }

        public SecTaskModel()
        {
            cardTasks = new List<CardTask>();
        }
    }
}
