﻿using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Models
{
    public class APIDBContect : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }

        public APIDBContect(DbContextOptions<APIDBContect> options)
            : base(options) { }
    }
}
