namespace MVS.Models
{
    public class InputModel
    {
        public string Title { get; set; }       // Заголовок новости
        public string Content { get; set; }     // Текст новости
        public string Category { get; set; }    // Название категории (или CategoryId)
    }
}
