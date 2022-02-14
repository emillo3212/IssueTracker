using Application.Dto.UsersDto;
using Application.Interfaces;
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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("Register")]
        public IActionResult Register(CreateUserDto newUser)
        {
            var user = _accountService.CreateUser(newUser);

            return Created($"api/accounts/{user.Id}", user);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUserDto loginUser)
        {

            return Ok(_accountService.Login(loginUser));
        }
    }
}
