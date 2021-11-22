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
    public class CreateProjectDto : IMap
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProjectUserDto> Users { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProjectDto, Project>();
        }
    }
}
