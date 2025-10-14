using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Repositories
{
#pragma warning disable
    public class PostRepository : IPostRepository
    {
        private readonly APIDBContect _context;
        public PostRepository(APIDBContect context)
        {
            _context = context;
        }

        public Post Create(Post entity)
        {
            _context.Posts.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var  post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Exists(int id)
        {
            return _context.Posts.Any(p => p.Id == id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts
                .Include(p => p.Tags)
                .ToList();
        }

        public Post GetById(int id)
        {
            return _context.Posts
                .Include(p => p.Tags)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> GetPostsByTags(int tagId)
        {
            return _context.Posts
                .Include(p => p.Tags)
                .Where(p => p.Tags.Any(t => t.Id == tagId))
                .ToList();
        }

        public bool PostExists(string title)
        {
            return _context.Posts.Any (p => p.Title == title);
        }

        public Post Update(Post entity)
        {
            _context.Posts.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
