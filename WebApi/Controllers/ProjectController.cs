using Application.Dto.ProjectsDto;
using Application.Dto.ProjectUserDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        
        public ProjectController(IProjectService projectService,IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
           
            var projects = _projectService.GetAllProjects();

            return Ok(projects);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user=  _userService.GetCurrentUser();

            if(user.Projects.FirstOrDefault(x=>x.Id==id)!=null)
            {
                var project = _projectService.GetProjectById(id);
                if (project == null)
                    return NotFound();

                return Ok(project);
            }

            if(user.Role.Name=="admin"|| user.Role.Name == "demo")
            {
                var project = _projectService.GetProjectById(id);
                if (project == null)
                    return NotFound();
                return Ok(project);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateProjectDto newProject)
        {
            var user = _userService.GetCurrentUser();
            newProject.Users.Add(new ProjectUserUserDto { UserId = user.Id });

            var project = _projectService.CreateProject(newProject);
            return Created($"api/projects/{project.Id}", project);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update(UpdateProjectDto updateProject)
        {
            _projectService.UpdateProject(updateProject);
            return NoContent();
        }

        [Authorize(Roles = "admin,demo")]
        [HttpDelete]
        public IActionResult Delete(DeleteProjectDto deleteProject)
        {
            var deleter = _userService.GetCurrentUser();
            _projectService.DeleteProject(deleteProject,deleter);
            return NoContent();
        }
    }
}
