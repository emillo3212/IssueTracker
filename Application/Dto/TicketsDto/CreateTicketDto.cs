using Application.Dto.ProjectsDto;
using Application.Dto.UsersDto;
using Application.Mappings;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dto.TicketsDto
{
    public class CreateTicketDto : IMap
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AssignToId { get; set; }
  
        public string Priority { get; set; }
        [JsonIgnore]
        public int CreatedById { get; set; }

        public int ProjectId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTicketDto, Ticket>();
            profile.CreateMap<Ticket, CreateTicketDto>();
            
           
        }
    }
}
