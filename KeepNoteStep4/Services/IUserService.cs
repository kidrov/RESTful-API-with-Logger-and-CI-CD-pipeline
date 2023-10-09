using Entities;

namespace Service
{
    /*
    * Should not modify this interface. You have to implement these methods in
    * corresponding Impl classes
    */

    public interface IUserService
    {
        bool RegisterUser(User user);
        bool UpdateUser(int userId,User user);
        User GetUserById(int userId);
        string GenerateJwtToken(int userId, string userName);
        bool ValidateUser(int userId, string password);
        bool DeleteUser(int userId);
    }
}
