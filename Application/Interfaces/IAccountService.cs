using Application.Dto.UsersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        UserDto CreateUser(CreateUserDto newUser);
        string Login(LoginUserDto loginUser);
    }
}
