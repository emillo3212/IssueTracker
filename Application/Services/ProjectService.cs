using Application.Dto;
using Application.Dto.ProjectsDto;
using Application.Dto.UsersDto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository,IUserRepository userRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProjectDto> GetAllProjects()
        {
            var projects = _projectRepository.GetAll();

            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public ProjectDto GetProjectById(int id)
        {
            var project = _projectRepository.GetById(id);
            return _mapper.Map<ProjectDto>(project);
        }

        public ProjectDto CreateProject(CreateProjectDto newProject)
        {
          
            var project = _mapper.Map<Project>(newProject);
           
            _projectRepository.Add(project);
            return _mapper.Map<ProjectDto>(project);
        }

        
    }
}
