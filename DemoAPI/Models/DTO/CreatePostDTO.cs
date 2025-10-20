using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreatePostDTO
    {
        [Required(ErrorMessage = "Заголовок обязателен")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Заголовок должен быть от 3 до 200 символов")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Содержание обязательно")]
        [MinLength(10, ErrorMessage = "Содержание должно быть не менее 10 символов")]
        public string Content { get; set; } = null!;

        public List<int>? TagIds { get; set; }
        public List<CreateTagDTO>? NewTags { get; set; }
    }
}
