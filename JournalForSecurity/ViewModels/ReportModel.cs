using JournalForSecurity.Data;
using JournalForSecurity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class ReportModel
    {
        public AppDbContext dbContext { get; set; }
        public List<Journal> Journals { get; set; }
        public List<CardTask> Tasks { get; set; }
        public List<CardEvent> Events { get; set; }


        public ReportModel(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ReportPages GetReport(string mode, int page)
        {

            int pageSize = 10;
            int count = 0;

            switch (mode)
            {
                case "journal":
                    {
                        Journals = dbContext.Journals
                            .Include(j => j.Department)
                            .Include(e => e.Explanation)
                            .ThenInclude(u => u.User)
                            .OrderByDescending(j => j.DateBegin)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
                        Tasks = new List<CardTask>();
                        Events = new List<CardEvent>();

                        count = dbContext.Journals.Count();
                        break;
                    }
                case "tasks":
                    {
                        Journals = new List<Journal>();
                        Tasks = dbContext.CardTasks
                            .Include(u => u.User)
                            .Include(d => d.Department)
                            .Include(e => e.Explanation)
                            .ThenInclude(u => u.User)
                            .OrderByDescending(j => j.DateBegin)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
                        Events = new List<CardEvent>();

                        count = dbContext.CardTasks.Count();
                        break;
                    }
                case "events":
                    {
                        Journals = new List<Journal>();
                        Tasks = new List<CardTask>();
                        Events = dbContext.CardEvents
                            .Include(u => u.User)
                            .Include(e => e.Department)
                            .OrderByDescending(j => j.Date)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

                        count = dbContext.CardEvents.Count();
                        break;
                    }
                default:
                    {
                        Journals = dbContext.Journals
                            .Include(j => j.Department)
                            .Include(e => e.Explanation)
                            .ThenInclude(u => u.User)
                            .OrderByDescending(j => j.DateBegin)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
                        Tasks = dbContext.CardTasks
                            .Include(u => u.User)
                            .Include(d => d.Department)
                            .Include(e => e.Explanation)
                            .ThenInclude(u => u.User)
                            .OrderByDescending(j => j.DateBegin)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
                        Events = dbContext.CardEvents
                            .Include(u => u.User)
                            .Include(e => e.Department)
                            .OrderByDescending(j => j.Date)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

                        count = Math.Max(Math.Max(dbContext.Journals.Count(), dbContext.CardTasks.Count()), dbContext.CardEvents.Count());

                        return new ReportPages(this, count, page, pageSize);
                    }
            }

            return new ReportPages(this, count, page, pageSize);
        }
    }
}
