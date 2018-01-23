using alfrek.api.Models.ApplicationUsers;
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
        public string ProfilePictureUrl { get; set; }
        public Affiliation Affiliation { get; set; }
        public List<SolutionListResource> Solutions { get; set; }

        public PublicProfileResource(string firstName, string lastName, string profilePictureUrl, Affiliation affiliation)
        {
            FirstName = firstName;
            LastName = lastName;
            ProfilePictureUrl = profilePictureUrl;
            Affiliation = affiliation;
            Solutions = new List<SolutionListResource>();
        }
    }
}
