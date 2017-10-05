namespace alfrek.api.Controllers.Resources.Input.Solutions
{
    public class SaveAuthorsResource
    {
        public string Email { get; set; }
        public string Name { get; set; }


        public SaveAuthorsResource(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}