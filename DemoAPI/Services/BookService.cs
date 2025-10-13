using DemoAPI.Models.DTO;
using DemoAPI.Models;
using DemoAPI.Repositories;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace DemoAPI.Services
{
#pragma warning disable
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;

        public BookService(IBookRepository br, IAuthorRepository ar)
        {
            _bookRepo = br;
            _authorRepo = ar;
        }

        private static BookDTO MapBookDTO(Book book )
        {
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                Author = book.Author == null ? null : new AuthorDTO
                {
                    Id = book.Author.Id,
                    Name = book.Author.Name,
                    Alias = book.Author.Alias
                }
            }; 
        }

        public BookDTO Create(CreateBookDTO createBookDTO)
        {
            int authorID;
            if (createBookDTO.AuthorId.HasValue) //автор существует
            {
                var existingAuthor = _authorRepo.GetById(createBookDTO.AuthorId.Value);
                if (existingAuthor == null)
                    throw new ArgumentException("Автор не найден");
                authorID = existingAuthor.Id;
            }
            else if (createBookDTO.newAuthor != null) //создаем автора вместе с книгйо
            {
                var newAuthor = new Author
                {
                    Name = createBookDTO.newAuthor.Name,
                    Alias = createBookDTO.newAuthor.Alias
                };

                var createdAuthor = _authorRepo.Create(newAuthor);
                authorID = createdAuthor.Id;
            }
            else
                throw new ArgumentException("необходимо создать " +
                    "автора или указать существующего");

            Book newBook = new Book
            {
                Title = createBookDTO.Title,
                AuthorId = authorID
            };

            var createdBook = _bookRepo.Create(newBook);
            return (MapBookDTO(createdBook));
        }


        public bool Delete(int id)
        {
            return _bookRepo.Delete(id);
        }

        public IEnumerable<BookDTO> GetAllbooks()
        {
            var books = _bookRepo.GetAll();
            return books.Select(MapBookDTO);
        }

        public BookDTO GetById(int id)
        {
            var book = _bookRepo.GetById(id);
            return book == null ? null : MapBookDTO(book);
        }

        public BookDTO Update(int id, UpdateBookDTO updateBookDTO)
        {
            var book = _bookRepo.GetById(id);
            if (book == null) return null;

            if (_authorRepo.Exists(updateBookDTO.AuthorId))
            {
                book.Title = updateBookDTO.Title;
                book.AuthorId = updateBookDTO.AuthorId;

                var updatesbook = _bookRepo.Update(book);
                return MapBookDTO(updatesbook);
            }
            else
                throw new ArgumentException("автор с таким id не был найден");
        }
    }
}
