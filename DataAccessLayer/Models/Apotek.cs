namespace DataAccessLayer.Models;

public class Apotek
{
    public int Id { get; set; }
    public String Navn { get; set; }
    public String Adresse { get; set; }
    public virtual List<ReceptUdlevering> ReceptUdleveringer { get; set; }
}