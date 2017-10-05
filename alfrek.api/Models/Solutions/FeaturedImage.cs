using System.ComponentModel.DataAnnotations.Schema;

namespace alfrek.api.Models.Solutions
{
    [Table("FeaturedImages")]
    public class FeaturedImage
    {
        public int Id { get; set; }

        public string PreviewUrl { get; set; }
        public string ImageUrl { get; set; }

        public int SolutionId { get; set; }


        public FeaturedImage(string previewUrl, string imageUrl)
        {
            PreviewUrl = previewUrl;
            ImageUrl = imageUrl;
        }
    }
}