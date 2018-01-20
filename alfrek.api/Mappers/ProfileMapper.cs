using alfrek.api.Controllers.Resources.View;
using alfrek.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alfrek.api.Mappers
{
    public static class ProfileMapper
    {
        public static PublicProfileResource ToPublicProfileResource(this ApplicationUser applicationUser)
        {
            return new PublicProfileResource(
                applicationUser.FirstName,
                applicationUser.LastName
            );
        }
    }
}
