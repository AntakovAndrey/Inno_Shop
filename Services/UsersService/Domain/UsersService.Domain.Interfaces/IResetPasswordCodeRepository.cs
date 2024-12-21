using UsersService.Domain.Core;

namespace UsersService.Domain.Interfaces;

public interface IResetPasswordCodeRepository
{
    ResetPasswordCode? GetResetPasswordCode(int id);
    ResetPasswordCode? GetResetPasswordCode(User user);
    void Create(ResetPasswordCode resetPasswordCode);
    void Update(ResetPasswordCode resetPasswordCode);
    void Delete(int id);
    void Save();
}