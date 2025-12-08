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

    public List<DTORecept> GetByCpr(string cprNummer)
    {
        return _receptRepository.GetByCpr(cprNummer).Select(recept => ReceptMapper.Map(recept)).ToList();
    }
    public List<DTORecept> GetAll()
    {
        return _receptRepository.GetAll().Select(recept => ReceptMapper.Map(recept)).ToList();
    }
    
    public DTORecept GetById(int id)
    {
        var DALrecept = _receptRepository.GetById(id);
        return ReceptMapper.Map(DALrecept);
    }
    
    public void Add(DTORecept recept)
    {
        var DALrecept = ReceptMapper.Map(recept);
        _receptRepository.Add(DALrecept);
    }
    
    public void Update(int id, DTORecept recept)
    {
        var DALrecept = ReceptMapper.Map(recept);
        _receptRepository.Update(id, DALrecept);
    }
    
    public void Delete(int id)
    {
        _receptRepository.Delete(id);
    }
}