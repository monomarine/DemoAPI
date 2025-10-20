using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateTagDTO
    {
        [Required(ErrorMessage = "Имя тега обязательно")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Имя должно быть минимум 4 символа")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
