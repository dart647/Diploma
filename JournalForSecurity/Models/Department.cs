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

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        public List<Journal> Journal { get; set; }

        public List<CardEvent> CardEvents { get; set; }

        public List<CardRequest> CardRequests { get; set; }

        public List<CardTask> CardTasks { get; set; }

        public List<ExplanatoryNote> ExplanatoryNotes { get; set; }

        public Department()
        {
            Journal = new List<Journal>();
            CardTasks = new List<CardTask>();
            CardRequests = new List<CardRequest>();
            CardEvents = new List<CardEvent>();
        }

        public override bool Equals(object obj)
        {
            return obj is Department department &&
                   Id == department.Id &&
                   Name == department.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
