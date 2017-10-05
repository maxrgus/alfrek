using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using alfrek.api.Models;

namespace alfrek.api.Services.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> GetToken(ApplicationUser user);
    }
}