using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateBookDTO
    {
        [Required(ErrorMessage = "Название книги обязательно")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 200 символов")]
        public string Title { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Идентификатор автора должен быть положительным числом")]
        public int? AuthorId { get; set; }

      
        public CreateAuthorDTO? newAuthor { get; set; }
    }
}