using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Models
{
    public class CardRequest
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не введено название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Desc { get; set; }

        [Display(Name = "Решение")]
        public bool Answer { get; set; }

        [Display(Name = "Отправитель")]
        public User User { get; set; }
        public string UserId { get; set; }

        public Department Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
