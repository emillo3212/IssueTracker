using Application.Dto.UsersDto;
using Application.Helpers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Validation
{
    public class AccountServiceValidation
    {
        public static void CreateUserValidation(CreateUserDto newUser,User user)
        {
            if (string.IsNullOrEmpty(newUser.FirstName) || string.IsNullOrEmpty(newUser.LastName) || string.IsNullOrEmpty(newUser.Email))
                throw new Exception("All fields have to be filled");

            if (user != null)
                throw new Exception("This email already exist!");

            if (!new EmailAddressAttribute().IsValid(newUser.Email))
                throw new Exception("This email is incorrect");
        }

        public static void LoginValidation(LoginUserDto loginUser, User user)
        {
            if (user == null)
                throw new Exception("Incorrect Email or password");

            if (PasswordHasher.Verify(loginUser.Password, user.Password) == false)
                throw new Exception("Incorrect Email or password");
        }
    }
}
