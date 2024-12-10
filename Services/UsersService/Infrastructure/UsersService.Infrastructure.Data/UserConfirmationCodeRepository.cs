using UsersService.Domain.Core;
using UsersService.Domain.Interfaces;

namespace UsersService.Infrastructure.Data
{
    public class UserConfirmationCodeRepository : IUserConfirmationCodeRepository
    {
        private readonly UsersServiceDBContext _dbContext;

        public UserConfirmationCodeRepository(UsersServiceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(UserConfirmationCode userConfirmationCode)
        {
            _dbContext.ConfirmationCodes.Add(userConfirmationCode);
        }

        public void Delete(int id)
        {
            var confirmationCode= _dbContext.ConfirmationCodes.First(x=>x.Id==id);
            _dbContext.ConfirmationCodes.Remove(confirmationCode);
        }

        public UserConfirmationCode GetConfirmationCode(int id)
        {
            return _dbContext.ConfirmationCodes.First(x=>x.Id == id);
        }

        public UserConfirmationCode? GetConfirmationCode(User user)
        {
            try
            {
                return _dbContext.ConfirmationCodes.First(x => x.User == user);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public void Update(UserConfirmationCode userConfirmationCode)
        {
            _dbContext.ConfirmationCodes.Update(userConfirmationCode);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}