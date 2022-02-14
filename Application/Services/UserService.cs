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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _config;


        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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


        public UserDto GetCurrentUser()
        {
   
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                if (userClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName) != null)
                {
                    var Id = int.Parse(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                    var user = _userRepository.GetById(Id);
                    return _mapper.Map<UserDto>(user);

                }
                  
                else
                    return null;
            }

            return null;
        }
       

    }
}
