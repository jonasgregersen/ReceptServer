using DALLægehus = DataAccessLayer.Models.Lægehus;
using DTOLægehus = DataTransferLayer.Models.Lægehus;

namespace DataAccessLayer.Mappers;

public class LægehusMapper
{
    public static DALLægehus Map(DTOLægehus lægehus)
    {
        return new DALLægehus
        {
            Id = lægehus.Id,
            Navn = lægehus.Navn,
            YderNummer = lægehus.YderNummer
        };
    }
    public static DTOLægehus Map(DALLægehus lægehus)
    {
        return new DTOLægehus
        {
            Id = lægehus.Id,
            Navn = lægehus.Navn,
            YderNummer = lægehus.YderNummer
        };
    }
}