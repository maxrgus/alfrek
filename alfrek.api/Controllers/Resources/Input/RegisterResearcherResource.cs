using alfrek.api.Models.ApplicationUsers;

namespace alfrek.api.Controllers.Resources.Input
{
    public class RegisterResearcherResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Research { get; set; }
        public Affiliation Affiliation { get; set; }
    }
}