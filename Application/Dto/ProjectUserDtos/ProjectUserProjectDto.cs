﻿using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.ProjectUserDtos
{
    public class ProjectUserProjectDto : IMap
    {
        public int ProjectId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProjectUser, ProjectUserProjectDto>();
        }
    }
}
