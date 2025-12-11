using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;
using DTORecept = DataTransferLayer.Models.Recept;

namespace Business.Models;

public class ReceptService
{
    private ReceptRepository _receptRepository;

    public ReceptService(ReceptRepository receptRepository)
    {
        _receptRepository = receptRepository;
    }

    public async Task<List<DTORecept>> GetByCpr(string cprNummer)
    {
        var cprRecepter = await _receptRepository.GetByCpr(cprNummer);
        return cprRecepter.Select(recept => ReceptMapper.Map(recept)).ToList();
    }
    public async Task<DTORecept> GetById(int id)
    {
        var recept = await _receptRepository.GetById(id);
        if (recept == null)
        {
            return null;
        }
        return ReceptMapper.Map(recept);
    }
    public async Task<List<DTORecept>> GetAll()
    {
        var recepter = await _receptRepository.GetAll();
        return recepter.Select(recept => ReceptMapper.Map(recept)).ToList();
    }
    public async Task<List<DTORecept>> GetLægehusRecepter(int id)
    {
        var DALRecepter = await _receptRepository.GetByLaegehusId(id);
        return DALRecepter.Select(r => ReceptMapper.Map(r)).ToList();
    }

    public static bool AlleOrdinationerUdleveret(DTORecept recept)
    {
        if (recept == null)
        {
            throw new ArgumentException("Recept not found");
        }
        foreach (var ordination in recept.Ordinationer)
        {
            if (ordination.FortagneUdleveringer < ordination.Udleveringer)
            {
                return false;
            }
        }
        return true;
    }


    public async Task Add(DTORecept recept)
    {
        var DALrecept = ReceptMapper.Map(recept);
        await _receptRepository.Add(DALrecept);
    }
    
    public async Task Update(DTORecept recept)
    {
        var DALrecept = ReceptMapper.Map(recept);
        await _receptRepository.Update(DALrecept);
    }
}