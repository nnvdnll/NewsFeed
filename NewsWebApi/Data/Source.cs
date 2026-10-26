using System.ComponentModel.DataAnnotations;

namespace NewsWebApi.Data
{
    public class Source
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
