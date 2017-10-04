namespace alfrek.api.Controllers.Resources.Input
{
    public class RegisterResource
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
        public RegisterResource(string email, string password)
        {
            Email = email;
            Password = password;
        }

    }
}