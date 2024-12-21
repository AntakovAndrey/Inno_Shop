using Microsoft.AspNetCore.Mvc;
using System.Text;
using UsersService.Domain.Core;
using UsersService.Domain.Interfaces;
using UsersService.Infrastructure.Data;
using UsersService.Services.Interfaces;

namespace API.Controllers
{
    [Route("UsersService/API/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public UserController(IUserRepository userRepository, IEmailService emailService, IUserService userService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            _userRepository.Create(user);
            _userRepository.Save();
            return Ok();
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_userRepository.GetUser(id));
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userRepository.GetUsers());
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user;
            try
            {
                user=_userService.Authenticate(email, password);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
            var token = await _userService.GenerateTokenAsync(user);
            return Ok(token);
        }

        [HttpGet("requestConfirmationCode/{userId}")]
        public async Task<IActionResult> RequestConfirmationCode(int userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                return BadRequest("User not found!");
            }
            var code = _userService.CreateConfirmationCode(user);
            await _emailService.SendMessage(user.Email, "Inno shop confirmation code", $"Your Inno shop confirmation code is {code}");
            return Ok();
        }
        
        [HttpGet("confirmEmail")]
        public IActionResult ConfirmEmail(string email, string confirmationCode)
        {   
            return _userService.ConfirmEmail(email,confirmationCode)?
                Ok("Email confirmed successfully!"):
                BadRequest("Failed to confirm Email!");
        }

        [HttpGet("requestPasswordResetCode/{userId}")]
        public async Task<IActionResult> RequestPasswordResetCode(int userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                return BadRequest("User not found!");
            }
            var code=_userService.CreateResetPasswordCode(user);
            await _emailService.SendMessage(user.Email, "Inno shop password reset code", $"Your password reset code is {code}");
            return Ok();
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string resetPasswordCode, string password)
        {
            var user = _userRepository.GetUsers().First(u=>u.Email == email);
            try
            {
                await _userService.ResetPassword(user,password,resetPasswordCode);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Password reset successfully!");
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, User user)
        {
            _userRepository.Update(user);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userRepository.Delete(id);
            return Ok();
        }
    }
}