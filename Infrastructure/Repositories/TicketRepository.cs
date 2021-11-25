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
    public class TicketRepository : ITicketRepository
    {
        private readonly IssueTrackerContext _context;

        public TicketRepository(IssueTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _context.Tickets.Include(u=>u.AssignTo).Include(p=>p.Project);
        }

        public Ticket GetById(int id)
        {
            return _context.Tickets.Include(u=>u.AssignTo).FirstOrDefault(x => x.Id == id);
        }

        public Ticket Add(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return ticket;
        }

        public void Update(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
        }

        public void Delete(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
        }
       
    }
}
