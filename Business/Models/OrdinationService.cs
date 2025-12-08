using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;
using DTOOrdination = DataTransferLayer.Models.Ordination;
namespace Business.Models;

public class OrdinationService
{
    private OrdinationRepository _ordinationRepository;
    
    public OrdinationService(OrdinationRepository ordinationRepository)
    {
        _ordinationRepository = ordinationRepository;
    }

    public List<DTOOrdination> GetAll()
    {
        return _ordinationRepository.GetAll().Select(o => OrdinationMapper.Map(o)).ToList();
    }

    public DTOOrdination GetById(int id)
    {
        return OrdinationMapper.Map(_ordinationRepository.GetById(id));
    }

    public void Add(DTOOrdination ordination)
    {
        var DALOrdination = OrdinationMapper.Map(ordination);
        _ordinationRepository.Add(DALOrdination);
    }

    public void Update(DTOOrdination ordination)
    {
        var DALOrdination = OrdinationMapper.Map(ordination);
        _ordinationRepository.Update(DALOrdination);
    }

    public void Delete(int id)
    {
        _ordinationRepository.Delete(id);
    }
}