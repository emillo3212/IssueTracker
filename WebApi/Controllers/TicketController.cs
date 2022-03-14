using Application.Dto.TicketsDto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;

        public TicketController(ITicketService ticketService,IUserService userService)
        {
            _ticketService = ticketService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var tickets = _ticketService.GetAllTickets();

            return Ok(tickets);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateTicketDto newTicketDto)
        {
            var ticket = _ticketService.CreateTicket(newTicketDto);
           

            return Created($"api/tickets/{ticket.Id}", ticket);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update(UpdateTicketDto updateTicket)
        {
            _ticketService.UpdateTicket(updateTicket);

            return NoContent();
        }
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(DeleteTicketDto ticket)
        {
            var deleter = _userService.GetCurrentUser();

            _ticketService.DeleteTicket(ticket,deleter);

            return Ok();
        }
    }
}
