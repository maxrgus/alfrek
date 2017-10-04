namespace alfrek.api.Controllers.Resources.Input
{
    public class LoginResource
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginResource(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}