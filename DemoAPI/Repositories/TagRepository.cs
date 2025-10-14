using DemoAPI.Models;

namespace DemoAPI.Repositories
{
#pragma warning disable
    public class TagRepository : ITagRepository
    {
        private readonly APIDBContect _context;
        public TagRepository(APIDBContect context)
        {
            _context = context;
        }

        public Tag Create(Tag entity)
        {
            _context.Tags.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var tag = _context.Tags.FirstOrDefault(tag =>  tag.Id == id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Exists(int id)
        {
            return _context.Tags.Any(tag => tag.Id == id);
        }

        public IEnumerable<Tag> GetAll()
        {
            return _context.Tags.ToList();
        }

        public Tag GetById(int id)
        {
            return _context.Tags.FirstOrDefault(t => t.Id == id);
        }

        public Tag? GetByName(string name)
        {
            var tag = _context.Tags.FirstOrDefault(t => t.Name == name);
            return tag;
        }

        public IEnumerable<Tag> GetTagsByNames(params string[] names)
        {
            return _context.Tags
                .Where(t => names.Contains(t.Name))
                .ToList();
        }

        public bool TagExists(string name)
        {
            return _context.Tags.Any (t => t.Name == name);
        }

        public Tag Update(Tag entity)
        {
           _context.Tags.Update(entity);
            _context.SaveChanges ();
            return entity;
        }
    }
}
