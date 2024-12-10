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
