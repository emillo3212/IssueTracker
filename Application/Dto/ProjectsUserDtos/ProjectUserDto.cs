using Application.Dto.ProjectsDto;
using Application.Dto.UsersDto;
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
    public class ProjectUserDto : IMap
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        //public UserInProjectDto User { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProjectUser, ProjectUserDto>().ForMember(dst=>dst.FirstName, opt=>opt.MapFrom(src => src.User.FirstName)).ForMember(dst=>dst.Id,opt=>opt.MapFrom(src=>src.UserId));
            profile.CreateMap<ProjectUserDto, ProjectUser>();
           
        }
    }
}
