using Application.Dto.UsersDto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

            user.Password = Hash(newUser.Passwrod, 1000);
            
            _userRepository.Add(user);
            return _mapper.Map<UserDto>(user);
        }

        private string Hash(string password, int iterations)
        {
            // Create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(20);

            // Combine salt and hash
            var hashBytes = new byte[16 + 20];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
        }

        private static bool Verify(string password, string hashedPassword)
        {

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(20);

            // Get result
            for (var i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }


    }
}
