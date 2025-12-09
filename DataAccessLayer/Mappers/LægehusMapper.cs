using DALLægehus = DataAccessLayer.Models.Lægehus;
using DTOLægehus = DataTransferLayer.Models.Lægehus;

namespace DataAccessLayer.Mappers;

public class LægehusMapper
{
    public static DALLægehus Map(DTOLægehus lægehus)
    {
        if (lægehus == null) return null!;
        return new DALLægehus
        {
            Id = lægehus.Id,
            Navn = lægehus.Navn,
            YderNummer = lægehus.YderNummer
        };
    }
    public static DTOLægehus Map(DALLægehus lægehus)
    {
        if (lægehus == null) return null!;
        return new DTOLægehus
        {
            Id = lægehus.Id,
            Navn = lægehus.Navn,
            YderNummer = lægehus.YderNummer
        };
    }
}