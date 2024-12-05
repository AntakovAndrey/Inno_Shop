using UsersService.Domain.Interfaces;
using UsersService.Domain.Core;

namespace UsersService.Infrastructure.Data
{
    public class UserRepository:IUserRepository
    {
        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }
        public void Create(User user)
        {
            throw new NotImplementedException();
        }
        public void Update(User user)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}