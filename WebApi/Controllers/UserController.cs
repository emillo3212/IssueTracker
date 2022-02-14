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
        
        public IActionResult Get(int id)
        {
            var user = _userService.GetUserById(id);
      
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [Authorize]
        [HttpGet("user")]
        public IActionResult CurrentUser()
        {
            var user = _userService.GetCurrentUser();

            if (user == null)
                return NotFound();

            return Ok(user);
        }
  

    }
}
