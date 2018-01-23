using System.ComponentModel.DataAnnotations.Schema;
using alfrek.api.Models.ApplicationUsers;

namespace alfrek.api.Models.Solutions
{
    [Table("Authors")]
    public class Author
    {
        public int Id { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ProfilePictureUrl { get; set; }

        public Affiliation Affiliation { get; set; }

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

        public Author(string firstName, string lastName, Affiliation affiliation)
        {
            FirstName = firstName;
            LastName = lastName;
            Affiliation = affiliation;
        }

        public Author(int id, string firstName, string lastName, string email, string name, Affiliation affiliation, int solutionId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Name = name;
            Affiliation = affiliation;
            SolutionId = solutionId;
        }

        public Author()
        {
        }
    }
}