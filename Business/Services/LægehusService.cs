using Business.Interfaces;
using DataAccessLayer.Mappers;
using DTOLægehus = DataTransferLayer.Models.Lægehus;
using DataAccessLayer.Interfaces;

namespace Business.Services;

public class LægehusService : ILægehusService
{
    private readonly ILægehusRepository _lægehusRepository;
    
    public LægehusService(ILægehusRepository lægehusRepository)
    {
        _lægehusRepository = lægehusRepository;
    }
    
    public async Task<List<DTOLægehus>> GetAll()
    {
        var lægehuse = await _lægehusRepository.GetAll();
        return lægehuse.Select(lægehus => LægehusMapper.Map(lægehus)).ToList();
    }
    
    public async Task<DTOLægehus> GetById(int id)
    {
        var DALLægehus = await _lægehusRepository.GetById(id);
        return LægehusMapper.Map(DALLægehus);
    }

    public async Task<DTOLægehus> GetByYdernummer(string ydernummer)
    {
        var DALLægehus = await _lægehusRepository.GetByYdernummer(ydernummer);
        if (DALLægehus == null)
        {
            return null!;
        }
        return LægehusMapper.Map(DALLægehus);
    }

    public async Task Add(DTOLægehus lægehus)
    {
        var DALlægehus = LægehusMapper.Map(lægehus);
        await _lægehusRepository.Add(DALlægehus);
    }
    
    public async Task Update(DTOLægehus lægehus)
    {
        var DALlægehus = LægehusMapper.Map(lægehus);
        await _lægehusRepository.Update(DALlægehus);
    }
}