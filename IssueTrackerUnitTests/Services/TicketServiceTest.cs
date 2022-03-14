using Application.Dto.RolesDto;
using Application.Dto.TicketsDto;
using Application.Dto.UsersDto;
using Application.Interfaces;
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
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IssueTrackerUnitTests.Services
{
    public class TicketServiceTest
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        Mock<ITicketRepository> _mockTicketRepository = new Mock<ITicketRepository>();

        IEnumerable<Ticket> tickets = new List<Ticket>()
            {
                new Ticket
                {
                    Id=1,
                    Name="ticket 1",
                    Description= "Description ticket 1"

                },
                new Ticket
                {
                    Id=2,
                    Name="ticket 2",
                    Description= "Description ticket 2"
                },
                new Ticket
                {
                    Id=3,
                    Name="ticket 3",
                    Description= "Description ticket 3"

                }

            };

        public TicketServiceTest()
        {
           
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();
            
           
        }


        [Fact]
        public void GetAllTickets_GetAll_ShouldReturnAllTickets()
        {
            _mockTicketRepository.Setup(x => x.GetAll()).Returns(tickets);

            var ticketService = new TicketService(_mockTicketRepository.Object, _mapper, _userService);

            var result = ticketService.GetAllTickets();

            result.Count().Should().Be(3);
        }

        [Theory]
        [InlineData("","test",3)]
        [InlineData("test","",3)]
        [InlineData("test","test",0)]
        [InlineData(" "," ",0)]
        public void CreateTicket_WhenCalledWithIncorrectData_ShouldThrowException(string name,string description,int assignTo)
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

            CreateTicketDto createTicketDto = new CreateTicketDto()
            {
                Name="siemak",
                Description="no nie"
            };
            var ticket = _mapper.Map<Ticket>(createTicketDto);

            Mock<IHttpContextAccessor> mocHttpContentAccessor = new Mock<IHttpContextAccessor>();
            mocHttpContentAccessor.Setup(h => h.HttpContext.User).Returns(claimsPrincipal);

            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(u => u.GetById(1)).Returns(user);

            _mockTicketRepository.Setup(t => t.Add(ticket)).Returns(ticket);

            UserService userService = new UserService(mockUserRepository.Object,_mapper,mocHttpContentAccessor.Object);

            TicketService ticketService = new TicketService(_mockTicketRepository.Object, _mapper, userService);

            Action result = ()=> ticketService.CreateTicket(createTicketDto);


            result.Should().Throw<Exception>();

        }
        [Fact]
        public void CreateTicket_WhenCalledWithcorrectData_ShouldCreateTicket()
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

            CreateTicketDto createTicketDto = new CreateTicketDto()
            { 
                Name = "siemak",
                Description = "no nie",
                AssignToId = 3
            };

            var ticket = _mapper.Map<Ticket>(createTicketDto);
            ticket.Id = 0;

            Mock<IHttpContextAccessor> mocHttpContentAccessor = new Mock<IHttpContextAccessor>();
            mocHttpContentAccessor.Setup(h => h.HttpContext.User).Returns(claimsPrincipal);

            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(u => u.GetById(1)).Returns(user);

            _mockTicketRepository.Setup(t => t.Add(ticket)).Returns(ticket);

            UserService userService = new UserService(mockUserRepository.Object, _mapper, mocHttpContentAccessor.Object);

            TicketService ticketService = new TicketService(_mockTicketRepository.Object, _mapper, userService);

            var result = ticketService.CreateTicket(createTicketDto);


            result.Id.Should().Be(0);

        }

        [Fact]
        public void UpdateTicket_WhenUpdate_ShouldUpdateTicket()
        {
            UpdateTicketDto updateTicketDto = new UpdateTicketDto()
            {
                Id = 1,
                Done = true
            };

            var ticket = _mapper.Map<Ticket>(updateTicketDto);

            _mockTicketRepository.Setup(t => t.GetById(1)).Returns(ticket);
            _mockTicketRepository.Setup(t => t.Update(ticket));

            var ticketService = new TicketService(_mockTicketRepository.Object, _mapper, _userService);

            ticketService.UpdateTicket(updateTicketDto);

            _mockTicketRepository.Verify(t => t.Update(ticket), Times.Once);

        }
        [Fact]
        public void DeleteTicket_WhenDelete_ShouldDeleteTicket()
        {
            DeleteTicketDto deleteTicketDto = new DeleteTicketDto() { Id = 1 };
            UserDto deleter = new UserDto() { Id = 1, Role = new RoleDto() { Name = "user" } };

            var ticket = _mapper.Map<Ticket>(deleteTicketDto);

            _mockTicketRepository.Setup(t => t.GetById(1)).Returns(ticket);
            _mockTicketRepository.Setup(t => t.Delete(ticket));

            var ticketService = new TicketService(_mockTicketRepository.Object, _mapper, _userService);
            ticketService.DeleteTicket(deleteTicketDto,deleter);

            _mockTicketRepository.Verify(t => t.Delete(ticket), Times.Once);

        }
    }
}
