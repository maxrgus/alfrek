namespace alfrek.api.Controllers.Resources.Input
{
    public class RegisterResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Organization { get; set; }
        public string Role { get; set; }

        public RegisterResource(string firstName, string lastName, string email, string password, string organization, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Organization = organization;
            Role = role;
        }
    }
}