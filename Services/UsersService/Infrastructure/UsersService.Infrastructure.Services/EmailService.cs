using UsersService.Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;

namespace UsersService.Infrastructure.Services
{
    public class EmailService:IEmailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public EmailService(string host,int port,string username,string password) 
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
        }

        public async Task SendMessage(string email, string subject, string body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Inno shop", _username));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_host, _port, MailKit.Security.SecureSocketOptions.Auto);
                await client.AuthenticateAsync(_username, _password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
