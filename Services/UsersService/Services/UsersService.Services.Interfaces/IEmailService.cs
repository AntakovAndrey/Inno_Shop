namespace UsersService.Services.Interfaces;

public interface IEmailService
{
    Task SendMessage(string email, string subject, string body);
}