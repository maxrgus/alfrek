using System.ComponentModel.DataAnnotations.Schema;

namespace alfrek.api.Models.Solutions
{
    [Table("Authors")]
    public class Author
    {
        public int Id { get; set; }
        
        public string Email { get; set; }
        public string Name { get; set; }

        public int SolutionId { get; set; }

        
        public ApplicationUser User { get; set; }

        public Author(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public Author(string email, string name, ApplicationUser user)
        {
            Email = email;
            Name = name;
            User = user;
        }

        public Author()
        {
        }
    }
}