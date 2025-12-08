using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;
using DTOReceptUdlevering = DataTransferLayer.Models.ReceptUdlevering;

namespace Business.Models;

public class ReceptUdleveringService
{
    private ReceptUdleveringRepository _receptUdleveringRepository;

    public ReceptUdleveringService(ReceptUdleveringRepository repository)
    {
        _receptUdleveringRepository = repository;
    }

    public List<DTOReceptUdlevering> GetAll()
    {
        return _receptUdleveringRepository.GetAll().Select(r => ReceptUdleveringMapper.Map(r)).ToList();
    }

    public DTOReceptUdlevering GetById(int id)
    {
        var DALReceptUdlevering = _receptUdleveringRepository.GetById(id);
        return ReceptUdleveringMapper.Map(DALReceptUdlevering);
    }

    public void Add(DTOReceptUdlevering receptUdlevering)
    {
        var DALReceptUdlevering = ReceptUdleveringMapper.Map(receptUdlevering);
        _receptUdleveringRepository.Add(DALReceptUdlevering);
    }

    public void Update(DTOReceptUdlevering receptUdlevering)
    {
        var DALReceptUdlevering = ReceptUdleveringMapper.Map(receptUdlevering);
        _receptUdleveringRepository.Update(DALReceptUdlevering);
    }

    public void Delete(int id)
    {
        _receptUdleveringRepository.Delete(id);
    }
}