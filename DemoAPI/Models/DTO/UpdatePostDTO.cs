using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class UpdatePostDTO
    {
        [Required(ErrorMessage = "Заголовок поста обязателен")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Заголовок должен быть от 5 до 200 символов")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Содержание поста обязательно")]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "Содержание должно быть от 10 до 5000 символов")]
        public string Content { get; set; } = null!;

        public List<int> TagIds { get; set; } = new();
    }
}