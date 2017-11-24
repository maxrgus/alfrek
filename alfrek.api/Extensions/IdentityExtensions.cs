using System.Security.Claims;
using System.Security.Principal;

namespace alfrek.api.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetFirstName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity) identity).FindFirst("FirstName");
            return (claim != null) ? claim.Value : string.Empty;
        }
          
    }
}