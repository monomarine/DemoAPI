using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class CreateAuthorDTO
    {
        [Required (ErrorMessage = "имя автора обязательно")]
        [StringLength (100, MinimumLength = 6, ErrorMessage = "длина имени должна быть от 6 до 100")]
        public string Name { get; set; } = null!;

        [StringLength(100, MinimumLength = 6, ErrorMessage = "длина псевдонима должна быть от 6 до 100")]
        public string Alias { get; set; } = string.Empty;
    }

}
