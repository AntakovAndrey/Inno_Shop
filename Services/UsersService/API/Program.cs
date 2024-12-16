using UsersService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using UsersService.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<UsersServiceDBContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserConfirmationCodeRepository, UserConfirmationCodeRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSingleton<IEmailService, EmailService>(x => 
    new EmailService(
        host: builder.Configuration["EmailSender:Host"],
        port: Convert.ToInt32(builder.Configuration["EmailSender:Port"]),
        username: builder.Configuration["EmailSender:Username"],
        password: builder.Configuration["EmailSender:Password"]
    ));
builder.Services.AddSingleton<AuthOptions>(x =>
    new AuthOptions
    {
        Audience = builder.Configuration["Auth:Audience"],
        Expiration = TimeSpan.Parse(builder.Configuration["Auth:Expiration"]),
        Issuer = builder.Configuration["Auth:Issuer"],
        Secret = builder.Configuration["Auth:Secret"]
    });

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Auth:Issuer"],
            ValidAudience = builder.Configuration["Auth:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( builder.Configuration["Auth:Secret"]) )
        };
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();