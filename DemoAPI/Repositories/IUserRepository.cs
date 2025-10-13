namespace DemoAPI.Repositories;
using DemoAPI.Models;

public interface IUserRepository
{
    //CRUD -операции
    IEnumerable<User> GetAllUsers();
    User GetUserById(int id);
    User AddUser(User user);
    bool DeleteUser(int id);
    User UpdateUser(int id, User user);
}
