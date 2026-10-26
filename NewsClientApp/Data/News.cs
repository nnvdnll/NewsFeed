using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsClientApp.Data
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }

        [ForeignKey(nameof(Source))]
        public int SourceId { get; set; }
        public Source Source { get; set; }
        public string Content { get; set; }
    }
}
