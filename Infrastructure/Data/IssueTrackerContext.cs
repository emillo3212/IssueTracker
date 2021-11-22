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
                .WithMany(p => p.Users);
            modelBuilder.Entity<ProjectUser>()
                .HasOne<User>(pu => pu.User)
                .WithMany(u => u.Projects);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
                
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
       
    }
}
