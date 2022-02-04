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

        [HttpGet]
        public IActionResult Get()
        {
           
            var projects = _projectService.GetAllProjects();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public IActionResult Create(CreateProjectDto newProject)
        {
            var user = _userService.GetCurrentUser();
            newProject.Users.Add(new ProjectUserUserDto { UserId = user.Id });

            var project = _projectService.CreateProject(newProject);
            return Created($"api/projects/{project.Id}", project);
        }
    }
}
