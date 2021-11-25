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
    public class UserRepository : IUserRepository
    {
        private readonly IssueTrackerContext _context;

        public UserRepository(IssueTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(p=>p.Projects).ThenInclude(pp=>pp.Project).Include(r=> r.Role).Include(u=>u.Tickets);
        }

        public User GetById(int id)
        {
            return _context.Users.Include(r=>r.Role).Include(p=> p.Projects).ThenInclude(pr=>pr.Project).FirstOrDefault(x => x.Id == id);
        }

        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

       
    }
}
