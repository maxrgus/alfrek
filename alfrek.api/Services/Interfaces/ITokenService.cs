using System.IdentityModel.Tokens.Jwt;
using alfrek.api.Models;

namespace alfrek.api.Services.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GetToken(ApplicationUser user);
    }
}