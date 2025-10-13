using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Repositories
{
#pragma warning disable
    public class AuthorRepository : IAuthorRepository
    {
        private readonly APIDBContect _context;
        public AuthorRepository(APIDBContect context)
        {
            _context = context;
        }
        public bool AutorExist(string name)
        {
            return _context.Authors.Any(c => c.Name == name);   
        }

        public Author Create(Author entity)
        {
            _context.Authors.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var author = GetById(id);
            if(author == null) return false;

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id)
        {
            return _context.Authors.Any(a => a.Id == id);
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Authors.Include(a => a.Books).ToList();
        }

        public Author GetById(int id)
        {
            var result =  _context.Authors
                .Include(a => a.Books)
                .FirstOrDefault(a => a.Id == id);

            return result;
        }

        public Author Update(Author entity)
        {
            _context.Authors.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
