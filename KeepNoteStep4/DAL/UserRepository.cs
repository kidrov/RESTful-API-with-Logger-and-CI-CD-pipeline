using System;
using System.Linq;
using Entities;
using Exceptions;

namespace DAL
{
    // Repository class is used to implement all Data access operations
    public class UserRepository : IUserRepository
    {
        private readonly KeepDbContext _dbContext;

        public UserRepository(KeepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool RegisterUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateUser(User user)
        {
            var existingUser = _dbContext.Users.Find(user.UserId);
            if (existingUser != null)
            {
                // Update properties of existingUser
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public User GetUserById(int userId)
        {
            var user = _dbContext.Users.Find(userId);
            if (user == null)
            { 
                throw new UserNotFoundException($"User with ID {userId} not found.");
            }

            return user;
        }


        public bool ValidateUser(int userId, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId && u.Password == password);
            return user != null;
        }


        public bool DeleteUser(int userId)
        {
            var user = _dbContext.Users.Find(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
