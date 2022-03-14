using Application.Dto.TicketsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Validation
{
    public static class TicketServiceValidation
    {
        public static void CreateTicketValidation(CreateTicketDto newTiket)
        {
            if (string.IsNullOrEmpty(newTiket.Name) || string.IsNullOrEmpty(newTiket.Description))
                throw new Exception("Ticket can not to have name or description empty");

            if(newTiket.AssignToId==0)
                throw new Exception("Ticket has to be assigned");
        }
    }
}
