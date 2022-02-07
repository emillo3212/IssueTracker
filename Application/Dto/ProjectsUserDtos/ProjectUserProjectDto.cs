using Application.Dto.ProjectsDto;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.ProjectsUserDtos
{
    public class ProjectUserProjectDto : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // public ProjectInUserDto project { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProjectUser, ProjectUserProjectDto>().ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.ProjectId))
                                                                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Project.Name));


        }
    }
}
