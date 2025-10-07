using DemoAPI.Models;

namespace DemoAPI.Services
{
    public class MockUserRepository : IUserRepository
    {
        private List<User> _users = new();
        public MockUserRepository()
        {
            _users.Add(new User { Id = 1, Email = "admin@mail.ru", Login = "admin" });
            _users.Add(new User { Id = 2, Email = "user23@mail.ru", Login = "user23" });
            _users.Add(new User { Id = 3, Email = "user54@mail.ru", Login = "user54" });
        }
        
        
        public User AddUser(User user)
        {
            if (user == null)
                return null;
            user.Id = _users.Count + 1;
            _users.Add(user);
            return user;
        }

        public bool DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return false;
            _users.Remove(user);
            return true;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return null;
            return user;
        }

        public User UpdateUser(int id, User user)
        {
            var updateUser = _users.FirstOrDefault(u => u.Id == id);
            if (UpdateUser == null)
                return null;
            updateUser.Login = user.Login;
            updateUser.Email = user.Email;

            return updateUser;
        }
    }
}
