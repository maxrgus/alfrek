using alfrek.api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public static ApplicationUser FindBySlugAsync(this UserManager<ApplicationUser> userManager, string slug)
        {
            return userManager?.Users?
                .Include(x => x.Affiliation)
                .SingleOrDefault(x => x.Slug == slug);
        }
          
    }
}