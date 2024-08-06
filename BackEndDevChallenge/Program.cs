using BackEndDevChallenge;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting.Server;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
var defaultConnection = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
builder.Services.AddDbContext<CalculatorContext>(options =>
       options.UseSqlServer(defaultConnection ?? config.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var serverUrl = "http://localhost:5195";
    var swaggerUrl = $"{serverUrl}/swagger";
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Navigate to the Swagger URL to explore the API documentation: {SwaggerUrl}", swaggerUrl);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
