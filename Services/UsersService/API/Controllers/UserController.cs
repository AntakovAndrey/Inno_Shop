using Microsoft.AspNetCore.Mvc;
using UsersService.Domain.Core;
using UsersService.Domain.Interfaces;
using UsersService.Infrastructure.Data;

namespace API.AddControllers
{
    [Route("UsersService/API/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
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