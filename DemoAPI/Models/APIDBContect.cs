using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Models
{
    public class APIDBContect : DbContext
    {
        public DbSet<User> Users { get; set; }

        public APIDBContect(DbContextOptions<APIDBContect> options)
            : base(options) { }
    }
}
