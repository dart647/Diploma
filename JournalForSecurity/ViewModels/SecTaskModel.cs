using JournalForSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class SecTaskModel
    {
        public List<CardTask> cardTasks { get; set; }

        public int CardTaskId { get; set; }

        public SecTaskModel()
        {
            cardTasks = new List<CardTask>();
        }
    }
}
