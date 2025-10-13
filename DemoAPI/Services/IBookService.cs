using DemoAPI.Models;
using DemoAPI.Models.DTO;

namespace DemoAPI.Services
{
    public interface IBookService
    {
        //дать все книги
        IEnumerable<BookDTO> GetAllbooks();
        //дать книгу по id
        BookDTO GetById(int id);
        //создать новую книгу
        BookDTO Create(CreateBookDTO createBookDTO);
        //обновить
        BookDTO Update(int id, UpdateBookDTO updateBookDTO);
        //удалить
        bool Delete(int id);
    }
}
