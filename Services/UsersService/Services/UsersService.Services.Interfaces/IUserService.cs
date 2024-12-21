using UsersService.Domain.Core;

namespace UsersService.Services.Interfaces
{
    public interface IUserService
    {
        public string CreateResetPasswordCode(User user);
        public string CreateConfirmationCode(User user);
        public Task<String> GenerateTokenAsync(User user);
        public User Authenticate(string email, string password);
        public bool ConfirmEmail(string email, string confirmationCode);
        public Task ResetPassword(User user, string newPassword,string resetPasswordCode);
    }
}