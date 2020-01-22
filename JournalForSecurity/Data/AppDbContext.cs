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
        public DbSet<CardRequest> CardRequests { get; set; }
        public DbSet<CardTask> CardTasks { get; set; }
        public DbSet<CardEvent> CardEvents { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ExplanatoryNote> ExplanatoryNotes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JournalRow>()
                .HasKey(t => new { t.JournalId, t.DepartmentId });

            modelBuilder.Entity<JournalRow>()
                .HasOne(sc => sc.Journal)
                .WithMany(s => s.JournalRows)
                .HasForeignKey(sc => sc.JournalId);

            modelBuilder.Entity<JournalRow>()
                .HasOne(sc => sc.Department)
                .WithMany(c => c.JournalRows)
                .HasForeignKey(sc => sc.DepartmentId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
