﻿using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DemoAPI.Repositories
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
        public bool DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u =>
            u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            else return false;

        }

        public User ExistUser(string loginOrEmail)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u =>
            u.Login == loginOrEmail ||
            u.Email == loginOrEmail);

            return user;
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u =>
            u.Id == id);
            if (user != null)
                return user;
            else return null;
        }

        public Role? RoleExist(int id)
        {
            return _context.Roles.FirstOrDefault(u => u.Id == id);
        }

        public User UpdateUser(int id, User user)
        {
            var userr = _context.Users.FirstOrDefault(u =>
            u.Id == id);
            if (userr != null)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            return user;
        }
            
    }
}
