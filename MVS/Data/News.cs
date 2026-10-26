using System.ComponentModel.DataAnnotations;

namespace MVS.Data
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
    }
}
