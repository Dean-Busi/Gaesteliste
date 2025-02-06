using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{

    public class TokenGenerator
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenGenerator(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));

        }

        public string CreateAccessToken(User user)
        {
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(30),
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var refreshToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(refreshToken);
        }
    }
}

