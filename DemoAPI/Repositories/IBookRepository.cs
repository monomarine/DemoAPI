using DemoAPI.Models;

namespace DemoAPI.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetBookByAuthor(int authorId);
    }
}
