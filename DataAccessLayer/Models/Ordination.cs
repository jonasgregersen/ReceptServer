namespace DataAccessLayer.Models;

public class Ordination
{
    public int Id { get; set; }
    public String Lægemiddel { get; set; }
    public String Dosis { get; set; }
    public int Udleveringer { get; set; }
    public int FortagneUdleveringer { get; set; }
    public Recept Recept { get; set; }
    public int ReceptId { get; set; }
}