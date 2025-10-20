using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreatePostDTO
    {
        [Required(ErrorMessage = "Заголовок обязателен")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Заголовок должен быть от 3 до 200 символов")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Содержание обязательно")]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "Содержание должно быть от 10 до 5000 символов")]
        public string Content { get; set; } = null!;

        // Для существующих тегов - валидация максимального количества
        [MaxLength(10, ErrorMessage = "Нельзя выбрать более 10 тегов")]
        public List<int>? TagIds { get; set; }

        // Для новых тегов - валидация количества и содержания
        [MaxLength(5, ErrorMessage = "Нельзя создать более 5 новых тегов одновременно")]
        public List<CreateTagDTO>? NewTags { get; set; }
    }
}
