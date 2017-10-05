using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using alfrek.api.Configuration;
using alfrek.api.Models;
using alfrek.api.Services.Interfaces;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace alfrek.api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<TokenConfiguration> _configuration;

        public TokenService(IOptions<TokenConfiguration> configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GetToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            return new JwtSecurityToken(_configuration.Value.Issuer,_configuration.Value.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_configuration.Value.Expiry),
                signingCredentials: creds);
        }
    }
}