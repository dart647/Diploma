using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Models
{
    public class JournalRow
    {
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }

        public Journal Journal { get; set; }
        public int? JournalId { get; set; }
    }
}
