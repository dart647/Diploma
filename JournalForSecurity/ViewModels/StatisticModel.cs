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
        
        public List<string> DepartmentNames { get; private set; }

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
            var statistic = new StatisticModel();
            statistic.DepartmentNames = this.dbContext.Departments.Select(n => n.Name).ToList();
            if (department != null)
            {
                statistic.JournalStrCount = dbContext.Journals.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count();
                statistic.JournalStrSuccessCount = dbContext.Journals.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count(j => j.Status);
                statistic.JournalStrUnsuccessCount = statistic.JournalStrCount - statistic.JournalStrSuccessCount;
                statistic.JournalStrPercent = statistic.JournalStrSuccessCount / statistic.JournalStrCount * 100;
                statistic.JournalStrExplanationCount = dbContext.ExplanatoryNotes.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count(en => en.TaskName.Contains("Обход"));
                statistic.TasksCount = dbContext.CardTasks.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count();
                statistic.TasksSuccessCount = dbContext.CardTasks.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count(t => t.State);
                statistic.TasksUnsuccessCount = statistic.TasksCount - statistic.TasksSuccessCount;
                statistic.TasksPercent = statistic.TasksUnsuccessCount / statistic.TasksCount * 100;
                statistic.TasksExplanationCount = dbContext.ExplanatoryNotes.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count(en => !en.TaskName.Contains("Обход"));
                statistic.EventsCount = dbContext.CardEvents.Include(d => d.Department).Where(d => d.Department.Equals(department)).Count();
            }
            else
            {
                statistic.JournalStrCount = dbContext.Journals.Count();
                statistic.JournalStrSuccessCount = dbContext.Journals.Count(j => j.Status);
                statistic.JournalStrUnsuccessCount = statistic.JournalStrCount - statistic.JournalStrSuccessCount;
                statistic.JournalStrPercent = statistic.JournalStrSuccessCount / statistic.JournalStrCount * 100;
                statistic.JournalStrExplanationCount = dbContext.ExplanatoryNotes.Count(en => en.TaskName.Contains("Обход"));
                statistic.TasksCount = dbContext.CardTasks.Count();
                statistic.TasksSuccessCount = dbContext.CardTasks.Count(t => t.State);
                statistic.TasksUnsuccessCount = statistic.TasksCount - statistic.TasksSuccessCount;
                statistic.TasksPercent = statistic.TasksUnsuccessCount / statistic.TasksCount * 100;
                statistic.TasksExplanationCount = dbContext.ExplanatoryNotes.Count(en => !en.TaskName.Contains("Обход"));
                statistic.EventsCount = dbContext.CardEvents.Count();
            }

            return statistic;
        }

    }
}
