namespace DemoAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Навигационное свойство для связи многие-ко-многим
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
