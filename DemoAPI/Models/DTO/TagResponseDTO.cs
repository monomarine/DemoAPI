using System.ComponentModel.DataAnnotations;
namespace DemoAPI.Models.DTO
{
    public class TagResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}