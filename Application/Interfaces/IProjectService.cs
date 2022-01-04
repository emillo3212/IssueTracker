using Application.Dto.ProjectsDto;
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
        

    }
}
