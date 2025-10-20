using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateBookDTO
    {
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Название от 2ти до 100 символов")]
        public string Title { get; set; } = null!;
        public int? AuthorId { get; set; }
        //ссылка на DTO автора
        public CreateAuthorDTO? newAuthor { get; set; }
    }
}
