using Business.Services;
using Business.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddScoped<ILægehusRepository, LægehusRepository>();
builder.Services.AddScoped<IReceptRepository, ReceptRepository>();

builder.Services.AddScoped<ILægehusService, LægehusService>();
builder.Services.AddScoped<IReceptService, ReceptService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())

{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();