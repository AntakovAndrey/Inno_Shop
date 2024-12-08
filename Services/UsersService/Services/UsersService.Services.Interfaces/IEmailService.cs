namespace UsersService.Services.Interfaces;

public interface IEmailService
{
    void SendMessage(string email, string subject, string body);
}
