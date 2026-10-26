using System.ComponentModel.DataAnnotations;

namespace NewsWebApi.Models
{
    public class NewsAdd
    {
        public string Title { get; set; }
        public string Category { get; set; }

        public int SourceId { get; set; }
        public string Content { get; set; }
    }
}
