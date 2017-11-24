using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using alfrek.api.Configuration;
using alfrek.api.Models;
using alfrek.api.Services.Interfaces;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace alfrek.api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<TokenConfiguration> _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IOptions<TokenConfiguration> configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<JwtSecurityToken> GetToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            
            claims.AddRange(userClaims);
            
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            return new JwtSecurityToken(_configuration.Value.Issuer,_configuration.Value.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_configuration.Value.Expiry),
                signingCredentials: creds);
        }
    }
}