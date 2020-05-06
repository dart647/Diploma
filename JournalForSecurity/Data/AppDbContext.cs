using JournalForSecurity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<CardTask> CardTasks { get; set; }
        public DbSet<CardEvent> CardEvents { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ExplanatoryNote> ExplanatoryNotes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
