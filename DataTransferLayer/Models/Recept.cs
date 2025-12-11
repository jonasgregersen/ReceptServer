

namespace DataTransferLayer.Models;

public class Recept
{
    public int Id { get; set; }
    public string YderNummer { get; set; }
    public string CprNummer { get; set; }
    public DateTime OprettelsesDato { get; set; }
    public int LægehusId { get; set; }
    public bool Lukket { get; set; }
    public List<Ordination> Ordinationer { get; set; }
}