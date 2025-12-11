using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using Business.Models;
using DataAccessLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer("Server=localhost,1433;Database=ReceptServer;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=True");
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<ApotekService>();
builder.Services.AddScoped<ReceptService>();

builder.Services.AddScoped<ApotekRepository>();
builder.Services.AddScoped<ReceptRepository>();
builder.Services.AddScoped<ReceptUdleveringRepository>();
builder.Services.AddScoped<OrdinationRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Apotek}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
