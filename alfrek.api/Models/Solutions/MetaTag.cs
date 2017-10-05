using System.ComponentModel.DataAnnotations.Schema;

namespace alfrek.api.Models.Solutions
{
    [Table("MetaTags")]
    public class MetaTag
    {
        public int Id { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }

        public int SolutionId { get; set; }
        
        public MetaTag(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}