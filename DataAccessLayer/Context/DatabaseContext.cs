using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context;

public class DatabaseContext: DbContext
{
    public DbSet<Apotek> Apoteker { get; set; }
    public DbSet<Lægehus> Lægehus { get; set; }
    public DbSet<Recept> Recepter { get; set; }
    public DbSet<ReceptUdlevering> ReceptUdleveringer { get; set; }
    public DbSet<Ordination> Ordinationer { get; set; }
    
    public DatabaseContext(DbContextOptions options): base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public DatabaseContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ReceptServer;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=True");
        }
    }

}