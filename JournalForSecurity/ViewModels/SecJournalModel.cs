using JournalForSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class SecJournalModel
    {
        public List<Journal> Journals { get; set; }

        public int JournalStrId { get; set; }

        public SecJournalModel()
        {
            Journals = new List<Journal>();
        }
    }
}
