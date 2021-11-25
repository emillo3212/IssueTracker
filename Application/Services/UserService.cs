using Application.Dto.RolesDto;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private IConfiguration _config;

        public UserService(IUserRepository userRepository, IMapper mapper,IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }

        public IEnumerable<UserDto> GetAllUser()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public UserDto GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
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
                throw new Exception("Incorrect Email or Password");


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
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

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public UserDto GetCurrentUser(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                var userClaims = identity.Claims;

                return new UserDto
                {
                    FirstName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    Role = _mapper.Map<RoleDto>(new Role { Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value } )
                };
            }

            return null;
        }

       

    }
}
