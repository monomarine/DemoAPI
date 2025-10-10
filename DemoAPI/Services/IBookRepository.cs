using DemoAPI.Models;

namespace DemoAPI.Services
{
    public interface IBookRepository
    {
        Book GetBookById(int id);
        Book CreateNewBook(Book book);
        Book UpdateBook(int id, Book book);
        void DeleteBook(int id);
        List<Book> GetAllBooks();
    }
}
