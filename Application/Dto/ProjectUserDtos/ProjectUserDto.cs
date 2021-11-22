using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
   public class ProjectUserDto : IMap
    {
        public int UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProjectUserDto, ProjectUser>();
            profile.CreateMap<ProjectUser, ProjectUserDto>();
        }
    }
}
