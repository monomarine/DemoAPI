namespace DemoAPI.Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        //ссылка на DTO автора
        public AuthorDTO? Author { get; set; }
    }
}
