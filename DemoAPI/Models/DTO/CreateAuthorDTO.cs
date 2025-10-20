using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateAuthorDTO
    {
        [Required(ErrorMessage ="Название автора обязательно")]
        [StringLength(50,MinimumLength = 10, ErrorMessage ="Имя должно быть от 10 символов")]
        public string Name { get; set; } = null!;

       
        public string Alias { get; set; } = string.Empty;
    }

}
