using CarrosAPI.Core.Interfaces.Repositories;
using CarrosAPI.Core.Interfaces.Services;
using CarrosAPI.Infra.Repository;
using CarrosAPI.Core.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CarrosAPI.Core.Repositories;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Configurações e serviços
builder.Services.AddControllers();
var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependências
builder.Services.AddScoped<ICarrosService, CarrosService>();
builder.Services.AddScoped<ICarrosRepository,CarroRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>(sp => new UserRepository("C:\\Users\\esther.pereira\\source\\repos\\CarrosAPI\\CarrosAPI.Infra\\DataBase\\User.csv"));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICSVRepository, CSVRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
    policy =>
    {
        policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();






