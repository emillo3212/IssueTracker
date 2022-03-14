using Application.Dto.ProjectsDto;
using Application.Dto.RolesDto;
using Application.Dto.UsersDto;
using Application.Mappings;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IssueTrackerUnitTests.Services
{
    
    public class ProjectServiceTest
    {
        private readonly IMapper _mapper;
        Mock<IProjectRepository> _mocProjectRepository = new Mock<IProjectRepository>();

        IEnumerable<Project> projects = new List<Project>()
        {
            new Project
            {
                Id=1,
                Name="Project 1",
                Description= "Description Project 1",
                Tickets = null,
                Users = null
            },
            new Project
            {
                Id=2,
                Name="Project 2",
                Description= "Description Project 2",
                Tickets = null,
                Users = null
            },
            new Project
            {
                Id=3,
                Name="Project 3",
                Description= "Description Project 3",
                Tickets = null,
                Users = null

            }

        };

        public ProjectServiceTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();
            
        }

      
        [Fact]
        public void GetAllProjects_GetAll_ShouldReturnAllProjects()
        {
            _mocProjectRepository.Setup(x => x.GetAll()).Returns(projects);

            var prjectService = new ProjectService(_mocProjectRepository.Object,_mapper);

            var result = prjectService.GetAllProjects();


            result.Count().Should().Be(3);
        }

        [Fact]
        public void GetProjectById_GetById_ShouldRetrunProjectWithId_2()
        {
            _mocProjectRepository.Setup(x=>x.GetById(2)).Returns(projects.FirstOrDefault(x=>x.Id==2));

            var projectService = new ProjectService(_mocProjectRepository.Object, _mapper);

            var result = projectService.GetProjectById(2);

            result.Id.Should().Be(2);
        }

        [Fact]
        public void CreateProject_WhenAdd_ShouldReturAddedProject()
        {
            Project ShouldBe = new Project()
            {
                Name = "Project 4",
                Description = "Description project 4"
            };
       
            CreateProjectDto createProjectDto = new CreateProjectDto()
            {
                Name = "Project 4",
                Description = "Description project 4"
            };
           
            var projectService = new ProjectService(_mocProjectRepository.Object, _mapper);

            var result = projectService.CreateProject(createProjectDto);


            result.Name.Should().Be(ShouldBe.Name);
            result.Description.Should().Be(ShouldBe.Description);
        }

        [Theory]
        [InlineData("","description2")]
        [InlineData("project2","")]
        public void CreateProject_WhenAddWithoutNameOrDescription_ShouldThrowException(string name,string description)
        {
            CreateProjectDto createProjectDto = new CreateProjectDto()
            {
                Name = name,
                Description = description
            };

            var projectService = new ProjectService(_mocProjectRepository.Object, _mapper);

            Action result = () => projectService.CreateProject(createProjectDto);


            result.Should().Throw<Exception>();

        }

        [Fact]
        public void UpdateProject_UpdateExistingProject_ShouldUpdate()
        {
            UpdateProjectDto updateProjectDto = new UpdateProjectDto()
            {
                Id = 2
            };

            var project = _mapper.Map<Project>(updateProjectDto);

            _mocProjectRepository.Setup(x => x.GetById(updateProjectDto.Id)).Returns(project);

            var projectService = new ProjectService(_mocProjectRepository.Object, _mapper);
            projectService.UpdateProject(updateProjectDto);

            _mocProjectRepository.Verify(x => x.Update(project), Times.Once);

        }

        [Fact]
        public void UpdateProject_UpdateNotExistingProject_ShouldNotRunUpdate()
        {
            UpdateProjectDto updateProjectDto = new UpdateProjectDto();

            var project = _mapper.Map<Project>(updateProjectDto);

            _mocProjectRepository.Setup(x => x.GetById(updateProjectDto.Id));


            var projectService = new ProjectService(_mocProjectRepository.Object, _mapper);
            projectService.UpdateProject(updateProjectDto);


            _mocProjectRepository.Verify(x => x.Update(project), Times.Never);

        }

        [Fact]
        public void DeleteProject_DeleteExistingProject_ShouldDeleteProject()
        {
            UserDto deleter = new UserDto() { Id = 1, Role = new RoleDto() { Name="rola"} };
            DeleteProjectDto deleteProject = new DeleteProjectDto();
            Project project = _mapper.Map<Project>(deleteProject);

            _mocProjectRepository.Setup(x => x.GetById(deleteProject.Id)).Returns(project);
            _mocProjectRepository.Setup(d => d.Delete(It.IsAny<Project>()));

            var projectService = new ProjectService(_mocProjectRepository.Object, _mapper);
            projectService.DeleteProject(deleteProject, deleter);

            _mocProjectRepository.Verify(d => d.Delete(project));
        }
       

    }
}
