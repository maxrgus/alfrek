using alfrek.api.Models.Solutions;
using System.Collections.Generic;

namespace alfrek.api.Controllers.Resources.View
{
    public class SolutionListResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ByLine { get; set; }
        public string Slug { get; set; }
        public Author Author { get; set; }
        public List<PurposedRole> Roles { get; set; }


        public SolutionListResource(int id, string title, string byLine, string slug, 
            List<PurposedRole> roles, 
            Author author)
        {
            Id = id;
            Title = title;
            ByLine = byLine;
            Slug = slug;
            Roles = roles;
            Author = author;
        }
    }
    
    
}