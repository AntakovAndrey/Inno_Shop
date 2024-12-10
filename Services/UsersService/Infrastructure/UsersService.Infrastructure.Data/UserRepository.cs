using UsersService.Domain.Interfaces;
using UsersService.Domain.Core;

namespace UsersService.Infrastructure.Data
{
    public class UserRepository:IUserRepository
    {
        private readonly UsersServiceDBContext _dbContext;

        public UserRepository(UsersServiceDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public User GetUser(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public void Create(User user)
        {
            _dbContext.Add(user);
        }

        public void Update(User user)
        {
            _dbContext.Update(user);
        }

        public void Delete(int id)
        {
            _dbContext.Users.Remove(GetUser(id));
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}