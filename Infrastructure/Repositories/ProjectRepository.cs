using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IssueTrackerContext _context;
        public ProjectRepository(IssueTrackerContext context)
        {
            _context = context;
        }
        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.Include(u=> u.Users).ThenInclude(us => us.User).Include(t=>t.Tickets).ThenInclude(c=>c.CreatedBy);
        }

        public Project GetById(int id)
        {
            return _context.Projects.Include(p=>p.Users).ThenInclude(pu=> pu.User).Include(t=>t.Tickets).ThenInclude(a=>a.CreatedBy).FirstOrDefault(x => x.Id == id);
        }

        public Project Add(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();

            return project;
        }

        public void Update(Project project)
        {
            _context.Projects.Update(project);

            _context.SaveChanges();
        }

        public void Delete(Project project)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }
       
    }
}
