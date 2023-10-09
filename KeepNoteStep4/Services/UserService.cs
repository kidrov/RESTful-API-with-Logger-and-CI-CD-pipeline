using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL;
using Entities;
using Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IConfiguration configuration)
        {
            _userRepository = repository;
            _configuration = configuration;
        }

        public bool DeleteUser(int userId)
        {
            // Check if the user exists
            var existingUser = _userRepository.GetUserById(userId);
            if (existingUser == null)
            {
                throw new UserNotFoundException($"User with ID {userId} not found.");
            }

            // Call the repository to delete the user
            return _userRepository.DeleteUser(userId);
        }

        public User GetUserById(int userId)
        {
            // Call the repository to get the user by ID
            return _userRepository.GetUserById(userId);
        }

        public bool RegisterUser(User user)
        {
            // Business logic and validation (if needed)
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            // Call the repository to register the user
            return _userRepository.RegisterUser(user);
        }

        public bool UpdateUser(int userId, User user)
        {
            // Check if the user exists
            var existingUser = _userRepository.GetUserById(userId);
            if (existingUser == null)
            {
                throw new UserNotFoundException($"User with ID {userId} not found.");
            }



            return _userRepository.UpdateUser(user);
        }

        public string GenerateJwtToken(int userId, string userName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new ClaimsIdentity(new Claim[]
            {
       new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
       new Claim(ClaimTypes.Name, userName)
            });

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Subject = claims,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public bool ValidateUser(int userId, string password)
        {

            var isValid = _userRepository.ValidateUser(userId, password);
            return isValid;
        }

    };
}