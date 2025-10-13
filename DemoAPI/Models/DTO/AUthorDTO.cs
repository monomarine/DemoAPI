namespace DemoAPI.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Alias { get; set; } = String.Empty;
    }
}
