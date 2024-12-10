using Microsoft.EntityFrameworkCore;
using UsersService.Domain.Core;
using UsersService.Domain.Core.Enums;

namespace UsersService.Infrastructure.Data
{
    public class UsersServiceDBContext:DbContext
    {
        public DbSet<User> Users{ get; set; }
        public DbSet<UserConfirmationCode> ConfirmationCodes { get; set; }

        public UsersServiceDBContext(DbContextOptions<UsersServiceDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}