using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.RolesDto
{
    public class RoleDto : IMap
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role,RoleDto>();
        }
    }
}
