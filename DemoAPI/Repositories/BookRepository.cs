using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Repositories
{
#pragma warning disable
    public class BookRepository : IBookRepository
    {
        private readonly APIDBContect _context;
        public BookRepository(APIDBContect context)
        {
            _context = context;
        }

        public Book Create(Book entity)
        {
            _context.Books.Add(entity); 
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Exists(int id)
        {
            return _context.Books.Any(x => x.Id == id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public IEnumerable<Book> GetBookByAuthor(int authorId)
        {
            return _context.Books
                .Include(b => b.Author)
                .Where(b =>  b.AuthorId == authorId)
                .ToList();
        }

        public Book GetById(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            return book;
        }

        public Book Update(Book entity)
        {
            _context.Books.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
