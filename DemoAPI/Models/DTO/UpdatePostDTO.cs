using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class UpdatePostDTO
    {
        [Required(ErrorMessage = "Заголовок обязателен")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Заголовок должен быть от 5 до 100 символов")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Содержание обязательно")]
        [MinLength(10, ErrorMessage = "Содержание должно быть не менее 10 символов")]
        public string Content { get; set; } = null!;

        public List<int> TagIds { get; set; } = new();
    }
}