using Application.Dto.ProjectUserDtos;
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
    public class UpdateProjectDto : IMap
    {
        public int Id { get; set; }
        public ICollection<ProjectUserUserDto> Users { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProjectDto, Project>();
        }
    }
}
