using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTrackingApplication.Models;

namespace TimeTrackingApplication.Data
{
    //THIS CLASS SETS UP THE DATABASE CONTEXT

    public class TimeTrackingContext : DbContext
    {
        public TimeTrackingContext(DbContextOptions<TimeTrackingContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PTORequest> PTORequests { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Overriding table name generation (avoiding gross table names)

            modelBuilder.Entity<Contact>()
                .ToTable("Contacts");
            modelBuilder.Entity<Employee>()
                .ToTable("Employees")
                .Property(e => e.PTO)
                .HasDefaultValue(80);
            modelBuilder.Entity<PTORequest>().ToTable("PTORequests");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Status>().ToTable("Statuses");
            modelBuilder.Entity<Models.Task>().ToTable("Tasks");
            modelBuilder.Entity<TimeEntry>().ToTable("TimeEntries");
        }
    }
}
