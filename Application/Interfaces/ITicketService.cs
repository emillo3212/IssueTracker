using Application.Dto.TicketsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITicketService
    {
        IEnumerable<TicketDto> GetAllTickets();
        TicketDto CreateTicket(CreateTicketDto newTiket);
        TicketDto GetTicketById(int id);
        void UpdateTicket(UpdateTicketDto updateTicket);
        void DeleteTicket(DeleteTicketDto ticket);
    }
}
