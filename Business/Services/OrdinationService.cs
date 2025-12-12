using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;
using DTOOrdination = DataTransferLayer.Models.Ordination;
namespace Business.Services;

public class OrdinationService
{
    private readonly OrdinationRepository _ordinationRepository;
    
    public OrdinationService(OrdinationRepository ordinationRepository)
    {
        _ordinationRepository = ordinationRepository;
    }

    public async Task<List<DTOOrdination>> GetAll()
    {
        var ordinationer = await _ordinationRepository.GetAll();
        return ordinationer.Select(o => OrdinationMapper.Map(o)).ToList();
    }

    public async Task<DTOOrdination> GetById(int id)
    {
        return OrdinationMapper.Map(await _ordinationRepository.GetById(id));
    }

    public async Task Add(DTOOrdination ordination)
    {
        var DALOrdination = OrdinationMapper.Map(ordination);
        await _ordinationRepository.Add(DALOrdination);
    }

    public async Task Update(DTOOrdination ordination)
    {
        var DALOrdination = OrdinationMapper.Map(ordination);
        await _ordinationRepository.Update(DALOrdination);
    }
}