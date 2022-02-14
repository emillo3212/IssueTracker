using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.TicketsDto
{
    public class UpdateTicketDto : IMap
    {
        public int Id { get; set; }
        public bool Done { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTicketDto, Ticket>();
        }
    }
}
