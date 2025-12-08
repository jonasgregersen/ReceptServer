using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;
using DTOApotek = DataTransferLayer.Models.Apotek;

namespace Business.Models;

public class ApotekService
{
    private readonly ApotekRepository _apotekRepository;
    
    public ApotekService(ApotekRepository apotekRepository)
    {
        _apotekRepository = apotekRepository;
    }
    
    public List<DTOApotek> GetAll()
    {
        return _apotekRepository.GetAll().Select(apotek => ApotekMapper.Map(apotek)).ToList();
    }
    
    public DTOApotek GetById(int id)
    {
        var DALapotek = _apotekRepository.GetById(id);
        return ApotekMapper.Map(DALapotek);
    }
    
    public void Add(DTOApotek apotek)
    {
        var DALapotek = ApotekMapper.Map(apotek);
        _apotekRepository.Add(DALapotek);
    }
    
    public void Update(DTOApotek apotek)
    {
        var DALapotek = ApotekMapper.Map(apotek);
        _apotekRepository.Update(DALapotek);
    }
    
    public void Delete(int id)
    {
        _apotekRepository.Delete(id);
    }
}