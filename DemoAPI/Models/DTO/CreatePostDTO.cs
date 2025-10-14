namespace DemoAPI.Models.DTO
{
    public class CreatePostDTO
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public List<int>? TagIds { get; set; } // существующие теги
        public List<CreateTagDTO>? NewTags { get; set; } // новые теги
    }
}
