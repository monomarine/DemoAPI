namespace DemoAPI.Models.DTO
{
    public class UpdatePostDTO
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public List<int> TagIds { get; set; } = new();
    }
}
