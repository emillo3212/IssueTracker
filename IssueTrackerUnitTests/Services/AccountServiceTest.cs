using Application.Dto.UsersDto;
using Application.Mappings;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IssueTrackerUnitTests.Services
{
    public class AccountServiceTest
    {
        Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly IMapper _mapper;
        private IConfiguration _config;
        public AccountServiceTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();

            var appSettingsStub = new Dictionary<string, string> 
            {
                {"Jwt:Key", "DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4"},
                {"Jwt:Issuer", "http://localhost:8084/"},
                {"Jwt:Audience", "http://localhost:8084/"}
            };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

        }

        [Theory]
        [InlineData("alick@gmail.com","alick","")]
        [InlineData("alick@gmail.com","","sdfsdf")]
        [InlineData("alick@gmail.com", "sdfsd","")]
        [InlineData("alick@.com", "sdfsd","sdfsdf")]
        [InlineData("", "sdfsd","sdfsdf")]
        public async Task CreateUser_WhenIncorrectData_ShouldThrowExcepiton(string email, string firstName, string lastName)
        {
            CreateUserDto createUserDto = new CreateUserDto()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            var user = _mapper.Map<User>(createUserDto);

            mockUserRepository.Setup(u => u.Add(user)).Returns(user);

            var accountService = new AccountService(mockUserRepository.Object, _mapper, _config);

            Func<Task> result = async () => await accountService.CreateUser(createUserDto);

            await result.Should().ThrowAsync<Exception>();

        }

        [Fact]
        public async Task CreateUser_WhenCorrectData_ShouldRetunCreatedUserEmail()
        {
            CreateUserDto createUserDto = new CreateUserDto()
            {
                Email = "nowy@gmail.com",
                FirstName = "Jan",
                LastName = "Kowalski"
            };

            var user = _mapper.Map<User>(createUserDto);

            mockUserRepository.Setup(u => u.Add(user)).Returns(user);

            var accountService = new AccountService(mockUserRepository.Object, _mapper, _config);

            var result = await accountService.CreateUser(createUserDto);


            result.Email.Should().Be("nowy@gmail.com");

        }

        [Theory]
        [InlineData("Siema@gmail.com","psss")]
        [InlineData("sdfdsiema@gmail.com","password")]
        public void Login_WhenEmailExist_ShouldThrowException(string email,string password)
        {
            LoginUserDto loginUserDto = new LoginUserDto()
            {
                Email = email,
                Password = password
            };
           
            IEnumerable<User> user = new List<User> { new User()
            {
                Email = "Siema@gmail.com",
                Password= "$MYHASH$V1$1000$GvP9RsSLcH + h5l8FB0wVyQavHRwaRw4WC / EGF / zI8A0D12Il",
                FirstName="siema",
                LastName ="siemowicz",
                Id=1,
                Role = new Role(){Name="rola"}
                
            } };

            mockUserRepository.Setup(u => u.GetAll()).Returns(user);


            var accountService = new AccountService(mockUserRepository.Object, _mapper, _config);

            Action result = () => accountService.Login(loginUserDto);

            result.Should().Throw<Exception>();


        }

        [Fact]
        public void Login_WhenCorrectData_ShouldNotThrowException()
        {
            LoginUserDto loginUserDto = new LoginUserDto()
            {
                Email = "Siema@gmail.com",
                Password = "password"
            };

            IEnumerable<User> user = new List<User> { new User()
            {
                Email = "Siema@gmail.com",
                Password= "$MYHASH$V1$1000$GvP9RsSLcH + h5l8FB0wVyQavHRwaRw4WC / EGF / zI8A0D12Il",
                FirstName="siema",
                LastName ="siemowicz",
                Id=1,
                Role = new Role(){Name="rola"}

            } };

            mockUserRepository.Setup(u => u.GetAll()).Returns(user);


            var accountService = new AccountService(mockUserRepository.Object, _mapper, _config);

            Action result = () => accountService.Login(loginUserDto);

            result.Should().NotThrow<Exception>();


        }
    }
}
