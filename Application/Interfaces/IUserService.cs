﻿using Application.Dto.UsersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUser();
        UserDto GetUserById(int id);
        UserDto CreateUser(CreateUserDto newUser);
    }
}
