namespace DataAccessLayer.Models;

public class Lægehus
{
    public int Id { get; set; }
    public String Navn { get; set; }
    public String YderNummer { get; set; }
    public virtual List<Recept> Recepter { get; set; }
}