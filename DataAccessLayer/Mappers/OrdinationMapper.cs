namespace DataAccessLayer.Mappers;
using DTOOrdination = DataTransferLayer.Models.Ordination;
using DALOrdination = DataAccessLayer.Models.Ordination;
public class OrdinationMapper
{
    public static DALOrdination Map(DTOOrdination ordination)
    {
        return new DALOrdination
        {
            Id = ordination.Id,
            Lægemiddel = ordination.Lægemiddel,
            Dosis = ordination.Dosis,
            Udleveringer = ordination.Udleveringer,
            FortagneUdleveringer = ordination.FortagneUdleveringer,
            ReceptId = ordination.ReceptId
        };
    }
    public static DTOOrdination Map(DALOrdination ordination)
    {
        return new DTOOrdination
        {
            Id = ordination.Id,
            Lægemiddel = ordination.Lægemiddel,
            Dosis = ordination.Dosis,
            Udleveringer = ordination.Udleveringer,
            FortagneUdleveringer = ordination.FortagneUdleveringer,
            ReceptId = ordination.ReceptId
        };
    }
}