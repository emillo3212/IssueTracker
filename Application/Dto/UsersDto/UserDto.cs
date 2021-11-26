using Application.Dto.ProjectsDto;
using Application.Dto.ProjectsUserDtos;
using Application.Dto.ProjectUserDtos;
using Application.Dto.RolesDto;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.UsersDto
{
    public class UserDto : IMap
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public RoleDto Role { get; set; }

        public ICollection<ProjectUserProjectDto> Projects { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>();
            profile.CreateMap<UserDto, User>();
        }
    }
}
