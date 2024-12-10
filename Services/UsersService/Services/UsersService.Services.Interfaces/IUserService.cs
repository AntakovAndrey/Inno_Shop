using UsersService.Domain.Core;

namespace UsersService.Services.Interfaces
{
    public interface IUserService
    {
        public string CreateConfirmationCode(User user);
        public Task<String> GenerateTokenAsync(User user);
        public bool ConfirmEmail(string email, string confirmationCode);
    }
}
