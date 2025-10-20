using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateAuthorDTO
    {
        [Required(ErrorMessage = "Имя автора обязательно")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 30 символов")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Псевдоним обязателен")]
        [StringLength(50, ErrorMessage = "Псевдоним не более 50 символов")]
        public string Alias { get; set; } = string.Empty;
    }
}