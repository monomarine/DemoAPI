namespace DemoAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Alias { get; set; } = String.Empty;

        //коллекционное навигационное свойство
        public List<Book> Books { get; set; } = new();
    }
}
