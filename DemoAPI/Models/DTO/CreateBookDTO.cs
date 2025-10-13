namespace DemoAPI.Models.DTO
{
    public class CreateBookDTO
    {
        public string Title { get; set; } = null!;
        public int? AuthorId { get; set; }
        //ссылка на DTO автора
        public CreateAuthorDTO? newAuthor { get; set; }
    }
}
