using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateTagDTO
    {
        [Required(ErrorMessage = "Название обязательно.")]
        [StringLength(50, ErrorMessage = "Название не может превышать 50 символов.")]
        public string Name { get; set; } = null!;

        [StringLength(200, ErrorMessage = "Описание не может превышать 200 символов.")]
        public string? Description { get; set; } 
    }
}
