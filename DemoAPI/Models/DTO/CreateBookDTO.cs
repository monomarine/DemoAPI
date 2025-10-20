using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateBookDTO
    {
        [Required(ErrorMessage = "Название книги обязательно")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Наименование должно быть от 10 символов")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Ссылка на автора обязательна")]
        public int? AuthorId { get; set; }
        //ссылка на DTO автора
        public CreateAuthorDTO? newAuthor { get; set; }
    }
}
