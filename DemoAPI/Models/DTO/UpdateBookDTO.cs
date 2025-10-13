namespace DemoAPI.Models.DTO
{
    public class UpdateBookDTO
    {
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
    }
}
