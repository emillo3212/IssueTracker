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
    public class CreateUserDto : IMap
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Passwrod { get; set; }
        public string ConfirmPasswrod { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, User>();
        }
    }
}
