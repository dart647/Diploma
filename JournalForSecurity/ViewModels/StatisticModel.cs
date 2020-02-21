using JournalForSecurity.Data;
using JournalForSecurity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class StatisticModel
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int JournalStrCount { get; set; }
        public double JournalStrSuccessCount { get; set; }
        public double JournalStrUnsuccessCount { get; set; }
        public double JournalStrPercent { get; set; }
        public int JournalStrExplanationCount { get; set; }
        public int TasksCount { get; set; }
        public double TasksSuccessCount { get; set; }
        public double TasksUnsuccessCount { get; set; }
        public double TasksPercent { get; set; }
        public int TasksExplanationCount { get; set; }
        public int EventsCount { get; set; }
        public Dictionary<Department, int> GuiltyDepartments { get; set; }

        public StatisticModel(AppDbContext dbContext)
        {
            //BeginDate = new DateTime(); В разработке 
            //EndDate = new DateTime();
            JournalStrCount = dbContext.Journals.Count();
            JournalStrSuccessCount = dbContext.Journals.Count(j => j.Status);
            JournalStrUnsuccessCount = JournalStrCount - JournalStrSuccessCount;
            JournalStrPercent = JournalStrSuccessCount / JournalStrCount * 100;
            JournalStrExplanationCount = dbContext.ExplanatoryNotes.Count(en => en.TaskName.Contains("Обход"));
            TasksCount = dbContext.CardTasks.Count();
            TasksSuccessCount = dbContext.CardTasks.Count(t => t.State);
            TasksUnsuccessCount = TasksCount - TasksSuccessCount;
            TasksPercent = TasksUnsuccessCount / TasksCount * 100;
            TasksExplanationCount = dbContext.ExplanatoryNotes.Count(en => !en.TaskName.Contains("Обход"));
            EventsCount = dbContext.CardEvents.Count();
            GuiltyDepartments = new Dictionary<Department, int>();
            foreach (var item in dbContext.Departments.Include(j => j.Journal))
            {
                GuiltyDepartments.Add(item, item.Journal.Count(j => !j.Status));
            }
        }
    }
}
