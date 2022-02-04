using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class IssueTrackerContext : DbContext
    {
        public IssueTrackerContext(DbContextOptions options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.ProjectId, pu.UserId });
            modelBuilder.Entity<ProjectUser>()
                .HasOne<Project>(pu => pu.Project)
                .WithMany(p => p.Users)
                .HasForeignKey(pu => pu.ProjectId);
            modelBuilder.Entity<ProjectUser>()
                .HasOne<User>(pu => pu.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(pu => pu.UserId);

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<Role>()
                .HasData(new Role() { Id = 1, Name = "user" });

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tickets);

            modelBuilder.Entity<Ticket>()
                .HasOne(u => u.AssignTo)
                .WithMany(t => t.Tickets);


            
                
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
       
    }
}
