using CarrosAPI.Interfaces.Repositories;
using CarrosAPI.Interfaces.Services;
using CarrosAPI.Repository;
using CarrosAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura��es e servi�os
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Depend�ncias
builder.Services.AddScoped<ICarrosService, CarrosService>();
builder.Services.AddScoped<ICarrosRepository, CarroRepository>();
builder.Services.AddSingleton<ICSVService, CSVService>();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Coloque UseRouting antes de UseAuthorization
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

