namespace DataAccessLayer.Models;

public class Recept
{
    public int Id { get; set; }
    public String YderNummer { get; set; }
    public String CprNummer { get; set; }
    public DateTime OprettelsesDato { get; set; }
    public virtual List<ReceptUdlevering> ReceptUdleveringer { get; set; }
    public List<Ordination> Ordinationer { get; set; }
    public Lægehus Lægehus { get; set; }
    public bool Lukket { get; set; }
    public int LægehusId { get; set; }
}