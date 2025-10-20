using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreatePostDTO
    {
        [Required(ErrorMessage = "Заголовок обязателен.")]
        [StringLength(200, ErrorMessage = "Заголовок не может превышать 200 символов.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Содержимое обязательно.")]
        public string Content { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "ID тегов должны быть положительными числами.")]
        public List<int>? TagIds { get; set; } // существующие теги

        public List<CreateTagDTO>? NewTags { get; set; } // новые теги
    }
}
