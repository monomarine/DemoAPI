using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateAuthorDTO
    {
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Имя от 5ти до 100 символов")]
        public string Name { get; set; } = null!;
        public string Alias { get; set; } = string.Empty;
    }

}
