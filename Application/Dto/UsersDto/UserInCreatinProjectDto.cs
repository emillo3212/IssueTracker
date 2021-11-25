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
    public class UserInCreatinProjectDto : IMap
    {
        public int Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserInCreatinProjectDto, User>();
            profile.CreateMap<User, UserInCreatinProjectDto>();
        }
    }
}
