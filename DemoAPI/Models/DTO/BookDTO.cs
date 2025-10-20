using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название книги обязательно")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть от 1 до 200 символов")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Идентификатор автора обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Идентификатор автора должен быть положительным числом")]
        public int AuthorId { get; set; }

        
        public AuthorDTO? Author { get; set; }
    }
}