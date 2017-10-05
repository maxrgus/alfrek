namespace alfrek.api.Controllers.Resources.View.Solutions
{
    public class AuthorResource
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public AuthorResource(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public AuthorResource()
        {
        }
    }
}