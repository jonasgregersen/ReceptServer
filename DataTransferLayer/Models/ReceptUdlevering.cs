namespace DataTransferLayer.Models;

public class ReceptUdlevering
{
    public int Id { get; set; }       
    public int ReceptId { get; set; }
    public int ApotekId { get; set; }
    public DateTime UdleveringsDato { get; set; }
    public int Antal { get; set; }
}