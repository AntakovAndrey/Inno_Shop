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