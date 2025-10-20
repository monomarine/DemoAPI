using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя автора обязательно")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 100 символов")]
        public string Name { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Псевдоним не должен превышать 50 символов")]
        public string Alias { get; set; } = String.Empty;
    }
}
