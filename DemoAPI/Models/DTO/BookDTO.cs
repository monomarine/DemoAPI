using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Название от 2ти до 100 символов")]
        public string Title { get; set; } 
        public int AuthorId { get; set; }
        //ссылка на DTO автора
        public AuthorDTO? Author { get; set; }
    }
}
