using Application.Dto.UsersDto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetAllUser();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "user")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetUserById(id);
      
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(CreateUserDto newUser)
        {
            
            var user = _userService.CreateUser(newUser);
            return Created($"api/users/{user.Id}", user);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto loginUser)
        {
            return Ok(_userService.Login(loginUser));
        }

       

    }
}
