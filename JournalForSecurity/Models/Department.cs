using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        public List<User> Users { get; set; }
        public List<CardEvent> CardEvents { get; set; }
        public List<CardRequest> CardRequests { get; set; }
        public List<CardTask> CardTasks { get; set; }

        [Display(Name = "Журнал")]
        public List<Journal> Journal { get; set; }

        public Department()
        {
            Users = new List<User>();
            Journal = new List<Journal>();
            CardTasks = new List<CardTask>();
            CardRequests = new List<CardRequest>();
            CardEvents = new List<CardEvent>();
        }
    }
}
