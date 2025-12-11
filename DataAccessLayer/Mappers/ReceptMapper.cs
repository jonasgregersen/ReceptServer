namespace DataAccessLayer.Mappers;
using DTORecept = DataTransferLayer.Models.Recept;
using DALRecept = Models.Recept;
using DTOOrdination = DataTransferLayer.Models.Ordination;
using DALOrdination = Models.Ordination;
using System.Collections.ObjectModel;
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
            LægehusId = recept.LægehusId,
            Lukket = recept.Lukket,
            Ordinationer = recept.Ordinationer.Select(OrdinationMapper.Map).ToList()
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
            LægehusId = recept.LægehusId,
            Lukket = recept.Lukket,
            Ordinationer = recept.Ordinationer.Select(OrdinationMapper.Map).ToList()
        };
    }
}