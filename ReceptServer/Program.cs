using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using Business.Services;
using DataAccessLayer.Repositories;

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

builder.Services.AddScoped<ApotekService>();
builder.Services.AddScoped<ReceptService>();

builder.Services.AddScoped<ApotekRepository>();
builder.Services.AddScoped<ReceptRepository>();
builder.Services.AddScoped<ReceptUdleveringRepository>();
builder.Services.AddScoped<OrdinationRepository>();


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
