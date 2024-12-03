using UsersService.Domain.Core.Enums;

namespace UsersService.Domain.Core
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}