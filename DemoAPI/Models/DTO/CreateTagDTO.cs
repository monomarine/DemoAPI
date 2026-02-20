using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateTagDTO
    {
        [Required(ErrorMessage = "Название тега обязательно")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Название тега должно быть от 2 до 50 символов")]
        public string Name { get; set; } = null!;

        [StringLength(200, ErrorMessage = "Описание тега не должно превышать 200 символов")]
        public string? Description { get; set; }
    }
}