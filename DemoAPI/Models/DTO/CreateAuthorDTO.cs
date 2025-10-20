using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateAuthorDTO
    {
        [Required(ErrorMessage = "Имя автора обязательно")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Имя должно быть от 1 до 100 символов")]
        public string Name { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Псевдоним должен быть до 50 символов")]
        public string Alias { get; set; } = string.Empty;
    }
}