﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Models
{
    public class ExplanatoryNote
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Explanation { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
