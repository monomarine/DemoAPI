using DemoAPI.Models;

public interface IAuthorRepository
{
    Author GetAuthorById(int id);
    Author UpdateAuthor(int id, Author author);
    void DeleteAuthor(int id);
    Author AddAuthor(Author author);
    List<Author> GetAllAuthors();

}
