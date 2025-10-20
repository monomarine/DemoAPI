using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateTagDTO
    {
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Название от 2ти до 100 символов")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
