namespace DataAccessLayer.Mappers;

using DALApotek = DataAccessLayer.Models.Apotek;
using DTOApotek = DataTransferLayer.Models.Apotek;

public class ApotekMapper
{
    public static DALApotek Map(DTOApotek apotek)
    {
        return new DALApotek
        {
            Id = apotek.Id,
            Navn = apotek.Navn,
            Adresse = apotek.Adresse
        };
    }
    public static DTOApotek Map(DALApotek apotek)
    {
        return new DTOApotek
        {
            Id = apotek.Id,
            Navn = apotek.Navn,
            Adresse = apotek.Adresse
        };
    }
}