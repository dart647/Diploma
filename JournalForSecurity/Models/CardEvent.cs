using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [StringLength(200, ErrorMessage = "Поле не должно превышать 200 символов")]
        [Required(ErrorMessage = "Не введено описание")]
        public string Desc { get; set; }

        [Display(Name = "Дата и время создания события")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Происшествие")]
        public bool IsAlertResult { get; set; }

        [Display(Name = "Описание происшествий")]
        public string AlertResult { get; set; }

        [Display(Name = "Отправитель")]
        public User User { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Отделение")]
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }

        [NotMapped]
        public List<string> Alerts { get; set; }
    }
}
