using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DemoAPI.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly APIDBContect _context;
        public UserRepository(APIDBContect context)
        {
            _context = context;
        }
        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
