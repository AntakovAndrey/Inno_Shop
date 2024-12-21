using UsersService.Domain.Core;

namespace UsersService.Domain.Interfaces
{
    public interface IUserConfirmationCodeRepository
    {
        UserConfirmationCode? GetConfirmationCode(int id);
        UserConfirmationCode? GetConfirmationCode(User user);
        void Create(UserConfirmationCode userConfirmationCode);
        void Update(UserConfirmationCode userConfirmationCode);
        void Delete(int id);
        void Save();
    }
}