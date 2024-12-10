using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersService.Domain.Core
{
    public class UserConfirmationCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
