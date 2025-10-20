using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class UpdatePostDTO
    {
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Название от 2ти до 100 символов")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Контент обязателен")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Контент от 5ти до 1000 символов")]
        public string Content { get; set; } = null!;
        public List<int> TagIds { get; set; } = new();
    }
}
