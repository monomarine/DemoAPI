using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class UpdatePostDTO
    {
        [Required(ErrorMessage = "Заголовок обязателен.")]
        [StringLength(200, ErrorMessage = "Заголовок не может превышать 200 символов.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Содержимое обязательно.")]
        public string Content { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо указать хотя бы один тег.")]
        public List<int> TagIds { get; set; } = new();
    }
}
