using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class UpdateBookDTO
    {
        [Required(ErrorMessage = "Название книги обязательно")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 100 символов")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "ID автора обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID автора должен быть положительным числом")]
        public int AuthorId { get; set; }
    }
}
