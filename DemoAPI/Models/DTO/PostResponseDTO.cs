using System.ComponentModel.DataAnnotations;
namespace DemoAPI.Models.DTO
{
    public class PostResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<TagResponseDTO> Tags { get; set; } = new();
    }
}