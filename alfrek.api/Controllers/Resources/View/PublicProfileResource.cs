using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alfrek.api.Controllers.Resources.View
{
    public class PublicProfileResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PublicProfileResource(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
