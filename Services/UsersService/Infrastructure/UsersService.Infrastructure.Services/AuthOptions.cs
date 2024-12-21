namespace UsersService.Infrastructure.Services;

public class AuthOptions
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public TimeSpan Expiration { get; set; }
}