using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersService.Domain.Core;
using UsersService.Infrastructure.Data;
using UsersService.Services.Interfaces;
using UsersService.Domain.Interfaces;

namespace UsersService.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserConfirmationCodeRepository _confirmationCodeRepository;

        public UserService(IUserRepository userRepository, IUserConfirmationCodeRepository confirmationCodeRepository) 
        { 
            _userRepository = userRepository;
            _confirmationCodeRepository = confirmationCodeRepository;
        }

        public User Authenticate(string email, string password)
        {
            var user = _userRepository.GetUsers().First(u=>u.IsEmailConfirmed==true&&u.Email==email&&u.Password==password);
            if(user==null)
                throw new Exception("Invalid email or password");
            return user;
        }

        public bool ConfirmEmail(string email, string confirmationCode)
        {
            User user = _userRepository.GetUsers().Where(u=>u.IsEmailConfirmed==false).First(u=>u.Email==email);
            if (user == null)
            {
                return false;
            }
            UserConfirmationCode storedConfirmationCode = _confirmationCodeRepository.GetConfirmationCode(user);
            if (storedConfirmationCode == null)
            {
                return false;
            }
            if(storedConfirmationCode.Code == confirmationCode)
            {
                user.IsEmailConfirmed = true;
                _userRepository.Update(user);
                _userRepository.Save();
                _confirmationCodeRepository.Delete(storedConfirmationCode.Id);
                _confirmationCodeRepository.Save();
                return true;
            }
            return false;
        }

        public string CreateConfirmationCode(User user)
        {
            var code = (Math.Abs((DateTime.Now.GetHashCode()+user.GetHashCode()).GetHashCode())%10000).ToString();
            UserConfirmationCode? confirmationCode = _confirmationCodeRepository.GetConfirmationCode(user);
            if (confirmationCode != null)
            {
                confirmationCode.Code = code;
                _confirmationCodeRepository.Update(confirmationCode);
            }
            else
            {
                confirmationCode = new UserConfirmationCode { Code = code, User = user, UserId = user.Id };
                _confirmationCodeRepository.Create(confirmationCode);
            }
            _confirmationCodeRepository.Save();
            return code;
        }


        public Task<string> GenerateTokenAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
