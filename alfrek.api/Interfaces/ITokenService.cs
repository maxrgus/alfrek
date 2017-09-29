using System.Collections.Generic;
using alfrek.api.Models;

namespace alfrek.api.Interfaces
{
    public interface ITokenService
    {
        string GetIdToken(ApplicationUser user);
        string GetAccessToken(string email);
    }
}