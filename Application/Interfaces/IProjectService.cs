using Application.Dto.ProjectsDto;
using Application.Dto.UsersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetAllProjects();
        ProjectDto GetProjectById(int id);
        ProjectDto CreateProject(CreateProjectDto newProject);
        void UpdateProject(UpdateProjectDto updateProject);
        string DeleteProject(DeleteProjectDto deleteProject,UserDto deleter);
        

    }
}
