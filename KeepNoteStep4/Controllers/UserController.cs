using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;

namespace KeepNote.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(UserController));

        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(User user)
        {
            try
            {
                _logger.Info("Registering user...");

                var result = _userService.RegisterUser(user);
                if (result)
                {
                    _logger.Info("User registered successfully.");
                    return Created("", user); // 201 Created
                }
                else
                {
                    _logger.Warn("User registration failed due to conflict.");
                    return Conflict(); // 409 Conflict
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred during user registration: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LoginUser(User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.Warn("Invalid user data provided.");
                    return BadRequest("Invalid user data");
                }

                var userName = user.UserName ?? ""; // Default to empty string if null
                var password = user.Password ?? ""; // Default to empty string if null

                var isValid = _userService.ValidateUser(user.UserId, password);
                if (isValid)
                {
                    _logger.Info("User login successful.");

                    var token = _userService.GenerateJwtToken(user.UserId, userName);
                    return Ok(new { Token = token }); // 200 OK with token
                }
                else
                {
                    _logger.Warn("User login failed due to invalid credentials.");
                    return Unauthorized(); // 401 Unauthorized
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred during user login: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, User user)
        {
            _logger.Info("UpdateUser method called.");
            try
            {
                // Call the UserService to update a user
                var result = _userService.UpdateUser(userId, user);
                if (result)
                {
                    return Ok(); // 200 OK
                }
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in UpdateUser: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating the user.");
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            _logger.Info("DeleteUser method called.");
            try
            {
                // Call the UserService to delete a user
                var result = _userService.DeleteUser(userId);
                if (result)
                {
                    return Ok(); // 200 OK
                }
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in DeleteUser: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the user.");
            }
        }


        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == userId.ToString())
                {
                    _logger.Info($"Getting user with ID: {userId}");

                    var user = _userService.GetUserById(userId);
                    if (user != null)
                    {
                        _logger.Info("User found.");
                        return Ok(user); // 200 OK
                    }
                    else
                    {
                        _logger.Warn("User not found.");
                        return NotFound(); // 404 Not Found
                    }
                }
                else
                {
                    _logger.Warn("Unauthorized access to user data.");
                    return Forbid(); // 403 Forbidden
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}