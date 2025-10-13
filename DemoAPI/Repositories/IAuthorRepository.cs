namespace DemoAPI.Repositories;
using DemoAPI.Models; 

public interface IAuthorRepository : IRepository<Author> 
{
    bool AutorExist(string name);
}
