using Business.Models;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer("Server=localhost,1433;Database=ReceptServer;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=True");
});

// Repository
builder.Services.AddScoped<LægehusRepository>();
builder.Services.AddScoped<ReceptRepository>();

// BLL
builder.Services.AddScoped<LægehusService>();
builder.Services.AddScoped<ReceptService>();

var app = builder.Build();

// Only use Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// ⭐ VERY IMPORTANT: This makes your controllers work!
app.MapControllers();

app.Run();