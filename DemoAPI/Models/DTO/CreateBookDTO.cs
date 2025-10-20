using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateBookDTO
    {
        [Required(ErrorMessage = "Название книги обязательно")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Название должно быть минимум 5 символов")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Наличие идентификатора обязательно")]
        public int? AuthorId { get; set; }
        //ссылка на DTO автора
        public CreateAuthorDTO? newAuthor { get; set; }
    }
}
