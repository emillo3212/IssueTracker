using Application.Dto.TicketsDto;
using Application.Dto.UsersDto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserService _userService;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository,IMapper mapper,IUserService userService)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public IEnumerable<TicketDto> GetAllTickets()
        {
            var tickets = _ticketRepository.GetAll();

            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public TicketDto CreateTicket(CreateTicketDto newTiket)
        {
        
            newTiket.CreatedById = _userService.GetCurrentUser().Id;
            var ticket = _mapper.Map<Ticket>(newTiket);

            ticket.Created = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            ticket.Done = false;
            _ticketRepository.Add(ticket);

            return _mapper.Map<TicketDto>(ticket);
            
        }

        public void UpdateTicket(UpdateTicketDto updateTicket)
        {
            var existingTicket = _ticketRepository.GetById(updateTicket.Id);
            var ticket = _mapper.Map(updateTicket, existingTicket);

            _ticketRepository.Update(ticket);
        }

        public TicketDto GetTicketById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTicket(DeleteTicketDto deleteTicket,UserDto deleter)
        {
           var Delticket = _ticketRepository.GetById(deleteTicket.Id);
            
            if (Delticket.ProjectId == 9)
                if(deleter.Role.Name=="demo")
                    throw new Exception("Nie masz uprawnień aby usunąć to zadanie!");

            var ticket = _mapper.Map<Ticket>(Delticket);
            _ticketRepository.Delete(ticket);
        }
    }
}
