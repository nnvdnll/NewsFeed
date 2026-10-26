namespace NewsClientApp.Models
{
    public class NewsAddDto
    {
       public string Title { get; set; }
       public string Category { get; set; }

        public int SourceId { get; set; }
        public string Content { get; set; }
    }
}
