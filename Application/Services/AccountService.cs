using Application.Dto.UsersDto;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(IUserRepository userRepository, IMapper mapper, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public UserDto CreateUser(CreateUserDto newUser)
        {

            if (_userRepository.GetAll().Any(u => u.Email == newUser.Email))
                throw new Exception("This email already exist!");

            if (!new EmailAddressAttribute().IsValid(newUser.Email))
                throw new Exception("this Email is incorrect");

            if (newUser.Passwrod.Length < 6)
                throw new Exception("Password must to have more than 6 chars");

            if (!string.Equals(newUser.Passwrod, newUser.ConfirmPasswrod))
                throw new Exception("Passwords are not the same");

            var user = _mapper.Map<User>(newUser);

            user.Password = PasswordHasher.Hash(newUser.Passwrod, 1000);
            user.RoleId = 1;
            _userRepository.Add(user);
            return _mapper.Map<UserDto>(user);
        }

        public string Login(LoginUserDto loginUser)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Email == loginUser.Email);

            if (user == null)
               throw new Exception("Incorrect Email or password");

            if (PasswordHasher.Verify(loginUser.Password, user.Password) == false)
                throw new Exception("Incorrect Email or password");


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            //_httpContextAccessor.HttpContext.Response.Cookies.Append("Jwt", jwt);

            return jwt;

        }
    }
}
