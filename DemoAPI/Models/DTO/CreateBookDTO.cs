using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateBookDTO
    {
        [Required(ErrorMessage = "Название книги обязательно")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Название должно быть от 2 до 100 символов")]
        public string Title { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "ID автора должен быть положительным числом")]
        public int? AuthorId { get; set; }

        public CreateAuthorDTO? NewAuthor { get; set; }
    }
}