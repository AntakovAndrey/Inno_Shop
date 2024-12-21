using UsersService.Domain.Core;
using UsersService.Domain.Interfaces;

namespace UsersService.Infrastructure.Data
{
    public class ResetPasswordCodeRepository:IResetPasswordCodeRepository
    {
        private readonly UsersServiceDBContext _dbContext;

        public ResetPasswordCodeRepository(UsersServiceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResetPasswordCode? GetResetPasswordCode(User user)
        {
            try
            {
                return _dbContext.ResetPasswordCodes.First(x => x.User == user);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public ResetPasswordCode? GetResetPasswordCode(int id)
        {
            try
            {
                return _dbContext.ResetPasswordCodes.First(x=>x.Id == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void Create(ResetPasswordCode resetPasswordCode)
        {
            _dbContext.ResetPasswordCodes.Add(resetPasswordCode);
        }

        public void Delete(int id)
        {
            var resetPasswordCode= _dbContext.ResetPasswordCodes.First(x=>x.Id==id);
            _dbContext.ResetPasswordCodes.Remove(resetPasswordCode);
        }

        public void Update(ResetPasswordCode resetPasswordCode)
        {
            _dbContext.ResetPasswordCodes.Update(resetPasswordCode);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}