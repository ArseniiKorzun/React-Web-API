using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        UserService _userService,
        IValidator<UserRegisterContract> _registerValidator,
        IValidator<UserLoginContract> _loginValidator) : ControllerBase
    {
        [HttpGet("users")]
        public ActionResult<List<UserResponse>> GetAll(){

            var users = _userService.GetAllUsers();
            var response = users.Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt
            }).ToList();


            return Ok(response);
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] UserRegisterContract data)
        {
            var validationResult = _registerValidator.Validate(data);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            if (_userService.EmailExists(data.Email))
                return Conflict("User with this email already exists.");

            var user = new User
            {
                Name = data.Name,
                Email = data.Email,
                PasswordHash = data.Password,
                CreatedAt = DateTime.UtcNow
            };
            _userService.AddUser(user);
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            });
        }

        [HttpPost("login")]
        public ActionResult<UserResponse> Login([FromBody] UserLoginContract data)
        {
            var validationResult = _loginValidator.Validate(data);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            var user = _userService.GetUserByEmail(data.Email);
            if (user == null || user.PasswordHash != data.Password)
                return Unauthorized("Invalid email or password.");

            return Ok(new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            });
        }

    }
}
