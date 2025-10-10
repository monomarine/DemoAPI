namespace DemoAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; } //внешний ключ зависимой сущности
        public Author Author { get; set; } = null!; //навигационное свойство
    }
}
