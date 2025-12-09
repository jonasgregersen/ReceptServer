using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;
using DataTransferLayer.Models;
using DTOLægehus = DataTransferLayer.Models.Lægehus;

namespace Business.Models;

public class LægehusService
{
    private readonly LægehusRepository _lægehusRepository;
    
    public LægehusService(LægehusRepository lægehusRepository)
    {
        _lægehusRepository = lægehusRepository;
    }
    
    public List<DTOLægehus> GetAll()
    {
        return _lægehusRepository.GetAll().Select(lægehus => LægehusMapper.Map(lægehus)).ToList();
    }
    
    public DTOLægehus GetById(int id)
    {
        var DALLægehus = _lægehusRepository.GetById(id);
        return LægehusMapper.Map(DALLægehus);
    }

    public async Task<DTOLægehus> GetByYdernummer(string ydernummer)
    {
        var DALLægehus = _lægehusRepository.GetByYdernummer(ydernummer);
        if (DALLægehus == null)
        {
            return null!;
        }
        return LægehusMapper.Map(DALLægehus);
    }

    public void Add(DTOLægehus lægehus)
    {
        var DALlægehus = LægehusMapper.Map(lægehus);
        _lægehusRepository.Add(DALlægehus);
    }
    
    public void Update(DTOLægehus lægehus)
    {
        var DALlægehus = LægehusMapper.Map(lægehus);
        _lægehusRepository.Update(DALlægehus);
    }
    
    public void Delete(int id)
    {
        _lægehusRepository.Delete(id);
    }
}