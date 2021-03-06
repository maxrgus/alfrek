﻿using alfrek.api.Models.ApplicationUsers;
using Microsoft.AspNetCore.Identity;

namespace alfrek.api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LinkedInId { get; set; }
        
        //Researcher specific
        public string ResearchField { get; set; }
        public Affiliation Affiliation { get; set; }
        public string Slug { get; set; }
        public string ProfilePictureUrl { get; set; }

        //Member specific
        public string Organization { get; set; }
        public string JobTitle { get; set; }
    }
}