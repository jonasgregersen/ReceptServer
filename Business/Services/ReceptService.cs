using Business.Interfaces;
using DataAccessLayer.Mappers;
using DataAccessLayer.Interfaces;
using DTORecept = DataTransferLayer.Models.Recept;

namespace Business.Services;

public class ReceptService : IReceptService
{
    private readonly IReceptRepository _receptRepository;

    public ReceptService(IReceptRepository receptRepository)
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
    public async Task<List<DTORecept>> GetByLægehusId(int id)
    {
        var DALRecepter = await _receptRepository.GetByLægehusId(id);
        return DALRecepter.Select(r => ReceptMapper.Map(r)).ToList();
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