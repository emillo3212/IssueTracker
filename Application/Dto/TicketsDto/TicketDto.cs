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
    public class TicketDto : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UserInProjectDto AssignTo { get; set; }
        public string Priority { get; set; }
        public string Created { get; set; }
        public bool Done { get; set; }
        public UserInProjectDto CreatedBy { get; set; }
        public int ProjectId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Ticket, TicketDto>();
            profile.CreateMap<UserDto, User>();
            profile.CreateMap<UserDto, Ticket>();
        }
    }
}
