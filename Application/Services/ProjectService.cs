using Application.Dto;
using Application.Dto.ProjectsDto;
using Application.Dto.ProjectUserDtos;
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
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
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
            if (newProject.Name.Length == 0)
                throw new Exception("Name can not be empty");
            if (newProject.Description.Length == 0)
                throw new Exception("Description can not be empty");

            var project = _mapper.Map<Project>(newProject);
            _projectRepository.Add(project);
            return _mapper.Map<ProjectDto>(project);
        }

        public void UpdateProject(UpdateProjectDto updateProject)
        {
            var existingProject = _projectRepository.GetById(updateProject.Id);

            if(existingProject!=null)
            {
                var project = _mapper.Map(updateProject, existingProject);

                _projectRepository.Update(project);
            }
            
        }

        public string DeleteProject(DeleteProjectDto deleteProject,UserDto delter)
        {
            var project = _projectRepository.GetById(deleteProject.Id);

            if(delter.Role.Name=="demo")
                if(project.Id==9)
                    throw new Exception("Jako użytkownik demo nie masz uprawnien do usunięcia tego projektu");
                
           
            if (project != null)
                _projectRepository.Delete(project);

            return "";
        }
    }
}
