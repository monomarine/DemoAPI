namespace DemoAPI.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        // Навигационное свойство для связи многие-ко-многим
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
