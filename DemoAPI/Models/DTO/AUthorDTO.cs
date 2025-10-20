using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class AuthorDTO
    {
        [Required(ErrorMessage = "ID обязателен")]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Имя от 5ти до 100 символов")]
        public string Name { get; set; } = null!;
        public string Alias { get; set; } = String.Empty;
    }
}
