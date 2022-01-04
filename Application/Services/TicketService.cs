using Application.Dto.TicketsDto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository,IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public IEnumerable<TicketDto> GetAllTickets()
        {
            var tickets = _ticketRepository.GetAll();

            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public TicketDto CreateTicket(CreateTicketDto newTiket)
        {
            var ticket = _mapper.Map<Ticket>(newTiket);
            ticket.Created = DateTime.UtcNow;
            ticket.Done = false;
            _ticketRepository.Add(ticket);

            return _mapper.Map<TicketDto>(ticket);
            
        }

    }
}
