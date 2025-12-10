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
    public DTORecept GetById(int id)
    {
        var recept = _receptRepository.GetById(id);
        if (recept == null)
        {
            return null;
        }
        return ReceptMapper.Map(recept);
    }
    public List<DTORecept> GetAll()
    {
        return _receptRepository.GetAll().Select(recept => ReceptMapper.Map(recept)).ToList();
    }
    public List<DTORecept> GetLægehusRecepter(int id)
    {
        var DALRecepter = _receptRepository.GetByLaegehusId(id);
        return DALRecepter.Select(r => ReceptMapper.Map(r)).ToList();
    }
    
    public void Add(DTORecept recept)
    {
        var DALrecept = ReceptMapper.Map(recept);
        _receptRepository.Add(DALrecept);
    }
    
    public void Update(DTORecept recept)
    {
        var DALrecept = ReceptMapper.Map(recept);
        _receptRepository.Update(DALrecept);
    }
    
    public void Delete(int id)
    {
        _receptRepository.Delete(id);
    }
}