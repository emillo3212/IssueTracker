using Application.Dto.ProjectsUserDtos;
using Application.Dto.TicketsDto;
using Application.Dto.UsersDto;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.ProjectsDto
{
    public class ProjectDto : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProjectUserDto> Users { get; set; } 

        public ICollection<TicketDto> Tickets { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDto>();
            profile.CreateMap<ProjectDto, Project>();
        }
    }
}
