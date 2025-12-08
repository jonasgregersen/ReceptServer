namespace DataAccessLayer.Mappers;
using DTORecept = DataTransferLayer.Models.Recept;
using DALRecept = DataAccessLayer.Models.Recept;

public class ReceptMapper
{
    public static DALRecept Map(DTORecept recept)
    {
        return new DALRecept
        {
            Id = recept.Id,
            YderNummer = recept.YderNummer,
            CprNummer = recept.CprNummer,
            OprettelsesDato = recept.OprettelsesDato,
            LægehusId = recept.LægehusId
        };
    }
    public static DTORecept Map(DALRecept recept)
    {
        return new DTORecept
        {
            Id = recept.Id,
            YderNummer = recept.YderNummer,
            CprNummer = recept.CprNummer,
            OprettelsesDato = recept.OprettelsesDato,
            LægehusId = recept.LægehusId
        };
    }
}