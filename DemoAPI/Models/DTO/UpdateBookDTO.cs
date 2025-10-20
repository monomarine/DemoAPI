using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class UpdateBookDTO
    {
        [Required(ErrorMessage = "Заголовок обязателен.")]
        [StringLength(200, ErrorMessage = "Заголовок не может превышать 200 символов.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "ID автора обязателен.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID автора должен быть положительным числом.")]
        public int AuthorId { get; set; }
    }
}
