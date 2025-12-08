namespace DataAccessLayer.Mappers;
using DTOReceptUdlevering = DataTransferLayer.Models.ReceptUdlevering;
using DALReceptUdlevering = DataAccessLayer.Models.ReceptUdlevering;

public class ReceptUdleveringMapper
{
    public static DALReceptUdlevering Map(DTOReceptUdlevering receptUdlevering)
    {
        return new DALReceptUdlevering
        {
            Id = receptUdlevering.Id,
            ReceptId = receptUdlevering.ReceptId,
            ApotekId = receptUdlevering.ApotekId,
            UdleveringsDato = receptUdlevering.UdleveringsDato,
            Antal = receptUdlevering.Antal
        };
    }
    public static DTOReceptUdlevering Map(DALReceptUdlevering receptUdlevering)
    {
        return new DTOReceptUdlevering
        {
            Id = receptUdlevering.Id,
            ReceptId = receptUdlevering.ReceptId,
            ApotekId = receptUdlevering.ApotekId,
            UdleveringsDato = receptUdlevering.UdleveringsDato,
            Antal = receptUdlevering.Antal
        };
    }
}