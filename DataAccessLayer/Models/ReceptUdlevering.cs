namespace DataAccessLayer.Models;

public class ReceptUdlevering
{
    public int Id { get; set; }
    public DateTime UdleveringsDato { get; set; }
    public int Antal { get; set; }
    
    public Apotek Apotek { get; set; }
    public int ApotekId { get; set; }
    
    public Recept Recept { get; set; }
    public int ReceptId { get; set; }
}