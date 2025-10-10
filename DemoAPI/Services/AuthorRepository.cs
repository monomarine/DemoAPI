using DemoAPI.Models;

namespace DemoAPI.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly APIDBContect _context;

        public AuthorRepository(APIDBContect context)
        {
            _context = context;
        }
        public Author AddAuthor(Author author)
        {
            if (author is null)
                throw new ArgumentNullException();
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }

        public void DeleteAuthor(int id)
        {
            var author = _context.Authors
                .FirstOrDefault(a => a.Id == id);

            if (author == null)
                return;

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

        public List<Author> GetAllAuthors()
        {
            return _context.Authors.ToList();
        }

        public Author GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }

        public Author UpdateAuthor(int id, Author author)
        {
            throw new NotImplementedException();
        }
    }
}
