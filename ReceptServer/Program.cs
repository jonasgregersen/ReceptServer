using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using Business.Services;
using Business.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IApotekService, ApotekService>();
builder.Services.AddScoped<IReceptService, ReceptService>();

builder.Services.AddScoped<IApotekRepository, ApotekRepository>();
builder.Services.AddScoped<IReceptRepository, ReceptRepository>();
builder.Services.AddScoped<IReceptUdleveringRepository, ReceptUdleveringRepository>();
builder.Services.AddScoped<IOrdinationRepository, OrdinationRepository>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())

{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();
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
