using DemoAPI.Models;

namespace DemoAPI.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly APIDBContect _context;

        public BookRepository(APIDBContect context)
        {
            _context = context;
        }
        public Book CreateNewBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book)+ "пуст");
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public Book UpdateBook(int id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}
