using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class PostResponseDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заголовок поста обязателен")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Заголовок должен быть от 1 до 200 символов")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Содержание поста обязательно")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "Содержание должно быть от 1 до 5000 символов")]
        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<TagResponseDTO> Tags { get; set; } = new();
    }
}