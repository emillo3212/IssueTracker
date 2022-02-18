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
using System.Net;
using System.Net.Mail;
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

        private readonly Random _random = new Random();
        public AccountService(IUserRepository userRepository, IMapper mapper, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<UserDto> CreateUser(CreateUserDto newUser)
        {
            if (newUser.FirstName == null || newUser.LastName == null || newUser.Email == null)
                throw new Exception("All fields have to be filled");

            if (_userRepository.GetAll().Any(u => u.Email == newUser.Email))
                throw new Exception("This email already exist!");

            if (!new EmailAddressAttribute().IsValid(newUser.Email))
                throw new Exception("this Email is incorrect");

            var passwordBuilder = new StringBuilder();
            passwordBuilder.Append(RandomString(6));

            newUser.Passwrod = passwordBuilder.ToString();

            await SendMailToUser(newUser.Email,newUser.Passwrod);

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


            return jwt;

        }
        
        private string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; 

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        
        private async Task SendMailToUser(string email,string pass)
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "issuetrackeremil@gmail.com",
                    Password = "issuetracker123"
                }
            };

            MailAddress fromEmail = new MailAddress("issuetrackeremil@gmail.com","IssueTrackerDemo");
            MailAddress toEmail = new MailAddress(email);

            MailMessage mailMessage = new MailMessage()
            {
                From = fromEmail,
                Subject = "Witaj w issueTracker",
                Body = $"Dzień dobry, właśnie zostałeś zarejestrowany do systemu issueTracker. Twoje dane logowania: \n Email: {email} \n" +
                $"Hasło: {pass} \n Już w krótce będziesz mógł zmienić swoje przypisane hasło w profilu urzytownika. \n Powodzenia."
            };

            mailMessage.To.Add(toEmail);

            try
            {
                client.Send(mailMessage);
            }
            catch (Exception error)
            {

                throw new Exception(error.ToString());
            }
           


        }
    }
}
