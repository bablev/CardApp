using AutoMapper;
using CardApp.BLL.Contracts;
using CardApp.BLL.Exceptions;
using CardApp.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly string _tokenKey;
        private readonly UserManager<AppUser> _userManager;
        private readonly string _audience;
        private readonly string _issuer;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _tokenKey = configuration["Jwt:Key"];
            _audience = configuration["Jwt:Audience"];
            _issuer = configuration["Jwt:Issuer"];
            _mapper = mapper;
        }
        public async Task<string> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var tokenHander = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_tokenKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                    Issuer = _issuer,
                    Audience = _audience,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)

                };
                var token = tokenHander.CreateToken(tokenDescriptor);
                return tokenHander.WriteToken(token);
            }

            return null;
        }
        public async Task RegistrationUser(RegistrationModel registrationModel)
        {
            var user = _mapper.Map<AppUser>(registrationModel);
            if (user == null)
            {
                throw new UserNotFoundException("User not found!");
            }
            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            user.PasswordHash = ph.HashPassword(user, registrationModel.Password);
            await _userManager.CreateAsync(user);
        }
    }
}
