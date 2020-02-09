using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Models
{
    public class CardEvent
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не введено название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Не введено описание")]
        public string Desc { get; set; }

        [Display(Name = "Дата и время")]
        public DateTime Date { get; set; }

        [Display(Name = "Происшествие")]
        public bool IsAlertResult { get; set; }
        
        [Display(Name = "Отправитель")]
        public User User { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Отделение")]
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
