using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();
        Ticket GetById(int id);
        Ticket Add(Ticket ticket);
        void Update(Ticket ticket);
        void Delete(Ticket ticket);
    }
}
