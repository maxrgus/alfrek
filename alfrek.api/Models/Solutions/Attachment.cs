using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alfrek.api.Models.Solutions
{
    [Table("Attachments")]
    public class Attachment
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Url { get; set; }

        public int SolutionId { get; set; }

        
        public Attachment(string title, string url)
        {
            Title = title;
            Url = url;
        }
    }
}