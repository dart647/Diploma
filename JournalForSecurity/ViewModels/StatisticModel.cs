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
        private AppDbContext dbContext { get; set; }
        public DateTime BeginDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int JournalStrCount { get; private set; }
        public double JournalStrSuccessCount { get; private set; }
        public double JournalStrUnsuccessCount { get; private set; }
        public double JournalStrPercent { get; private set; }
        public int JournalStrExplanationCount { get; private set; }
        public int TasksCount { get; private set; }
        public double TasksSuccessCount { get; private set; }
        public double TasksUnsuccessCount { get; private set; }
        public double TasksPercent { get; private set; }
        public int TasksExplanationCount { get; private set; }
        public int EventsCount { get; private set; }
        public Dictionary<Department, int> GuiltyDepartments { get; set; }

        public StatisticModel()
        {
        }

        public StatisticModel(AppDbContext dbContext)
        {
            //BeginDate = new DateTime(); В разработке 
            //EndDate = new DateTime();

            this.dbContext = dbContext;
        }

        public StatisticModel GetDepartmentStatistic(Department department)
        {
            StatisticModel statistic = new StatisticModel
            {
                JournalStrCount = dbContext.Journals.Include(d=>d.Department).Where(d=>d.Department.Equals(department)).Count(),
                JournalStrSuccessCount = dbContext.Journals.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count(j => j.Status),
                JournalStrUnsuccessCount = JournalStrCount - JournalStrSuccessCount,
                JournalStrPercent = JournalStrSuccessCount / JournalStrCount * 100,
                JournalStrExplanationCount = dbContext.ExplanatoryNotes.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count(en => en.TaskName.Contains("Обход")),
                TasksCount = dbContext.CardTasks.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count(),
                TasksSuccessCount = dbContext.CardTasks.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count(t => t.State),
                TasksUnsuccessCount = TasksCount - TasksSuccessCount,
                TasksPercent = TasksUnsuccessCount / TasksCount * 100,
                TasksExplanationCount = dbContext.ExplanatoryNotes.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count(en => !en.TaskName.Contains("Обход")),
                EventsCount = dbContext.CardEvents.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count()
            };

            return statistic;
        }

    }
}
