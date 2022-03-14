using Application.Dto.TicketsDto;
using Application.Dto.UsersDto;
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
        void UpdateTicket(UpdateTicketDto updateTicket);
        void DeleteTicket(DeleteTicketDto ticket,UserDto deleter);
    }
}
