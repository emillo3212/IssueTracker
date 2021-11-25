using Application.Dto.ProjectsDto;
using Application.Dto.UsersDto;
using Application.Mappings;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.TicketsDto
{
    public class CreateTicketDto : IMap
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AssignToId { get; set; }
        public Priority Priority { get; set; }
       
        public int ProjectId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTicketDto, Ticket>();
        }
    }
}
