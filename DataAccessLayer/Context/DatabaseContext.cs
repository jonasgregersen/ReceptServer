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
        modelBuilder.Entity<Lægehus>().HasData(
            new Lægehus { Id = 1, Navn = "Skive Lægehus", YderNummer = "123456" },
            new Lægehus { Id = 2, Navn = "Aarhus Sundhedscenter", YderNummer = "654321" },
            new Lægehus { Id = 3, Navn = "Viborg Lægeklinik", YderNummer = "112233" }
        );

        modelBuilder.Entity<Apotek>().HasData(
            new Apotek {Id = 1, Navn = "Sct. Clement Apotek", Adresse = "Sct. Clemens Torv 4, 8000 Aarhus C" },
            new Apotek {Id = 2, Navn = "Skive Apotek", Adresse = "Østergade 15, 7800 Skive" },
            new Apotek {Id = 3, Navn = "Holstebro Løve Apotek", Adresse = "Nørregade 20, 7500 Holstebro" }
        );
    }
}