using Application.Mappings;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IssueTrackerUnitTests.Services
{
    public class UserServiceTest
    {
        IMapper _mapper;
        Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        IHttpContextAccessor httpContextAccessor;

        IEnumerable<User> users = new List<User>()
        {
            new User()
            {
                Id=1,
                FirstName="Stasiu",
                LastName= "Staszewski"
            },
            new User()
            {
                Id=2,
                FirstName="Ala",
                LastName= "Aliszewska"
            },
            new User()
            {
                Id=3,
                FirstName="Arnold",
                LastName= "Arnoldowski"
            }
        };

        public UserServiceTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();

        }

        [Fact]
        public void GetAllUser_GetAll_ShouldReturnAllUsers()
        {
            mockUserRepository.Setup(u => u.GetAll()).Returns(users);

            var userService = new UserService(mockUserRepository.Object,_mapper, httpContextAccessor);

            var result = userService.GetAllUser();

            result.Count().Should().Be(3);
        }

        [Fact]
        public void GetUserById_GetById_ShouldReturnUserWithId_2()
        {
            User user = new User()
            {
                Id = 2,
            };

            mockUserRepository.Setup(u => u.GetById(2)).Returns(user);

            var userService = new UserService(mockUserRepository.Object, _mapper, httpContextAccessor);

            var result = userService.GetUserById(2);

            result.Id.Should().Be(2);
        }

        [Fact]
        public void GetCurrentUser_WhenCallGetCurrentUser_ShouldReturnCurrentUserWhoCalledMethod()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(
                      new ClaimsIdentity(
                          new Claim[] { new Claim(ClaimTypes.NameIdentifier, "1"),
                                         new Claim(ClaimTypes.GivenName,"jacek") })
                      );
            User user = new User()
            {
                Id = 1,
                FirstName = "jacek"
            };

            Mock<IHttpContextAccessor> mocHttpContentAccessor = new Mock<IHttpContextAccessor>();
            mocHttpContentAccessor.Setup(h => h.HttpContext.User).Returns(claimsPrincipal);

            mockUserRepository.Setup(u => u.GetById(1)).Returns(user);

            var userService = new UserService(mockUserRepository.Object, _mapper, mocHttpContentAccessor.Object);
            var result = userService.GetCurrentUser();

            result.Id.Should().Be(1);
        }
    }
}
