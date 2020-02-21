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
        public List<Journal> Journals { get; set; }
        public List<CardTask> Tasks { get; set; }
        public List<CardEvent> Events { get; set; }

        public ReportModel(AppDbContext dbContext, string mode)
        {
            switch (mode)
            {
                case "journal":
                    {
                        Journals = dbContext.Journals
                            .Include(j => j.Department)
                            .Include(e => e.Explanation)
                            .ThenInclude(u=>u.User)
                            .ToList();
                        Tasks = new List<CardTask>();
                        Events = new List<CardEvent>();
                        break;
                    }
                case "tasks":
                    {
                        Journals = new List<Journal>();
                        Tasks = dbContext.CardTasks
                            .Include(u=>u.User)
                            .Include(d => d.Department)
                            .Include(e => e.Explanation)
                            .ThenInclude(u => u.User)
                            .ToList();
                        Events = new List<CardEvent>();
                        break;
                    }
                case "events":
                    {
                        Journals = new List<Journal>();
                        Tasks = new List<CardTask>();
                        Events = dbContext.CardEvents
                            .Include(u => u.User)
                            .Include(e => e.Department)
                            .ToList();
                        break;
                    }
                default:
                    {
                        Journals = dbContext.Journals
                            .Include(j => j.Department)
                            .Include(e => e.Explanation)
                            .ThenInclude(u => u.User)
                            .ToList();
                        Tasks = dbContext.CardTasks
                            .Include(u => u.User)
                            .Include(d => d.Department)
                            .Include(e => e.Explanation)
                            .ThenInclude(u => u.User)
                            .ToList();
                        Events = dbContext.CardEvents
                            .Include(u => u.User)
                            .Include(e => e.Department)
                            .ToList();
                        break;
                    }
            }
        }
    }
}
