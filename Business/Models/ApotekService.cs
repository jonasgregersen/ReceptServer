using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;
using DataTransferLayer.Models;
using DTOApotek = DataTransferLayer.Models.Apotek;

namespace Business.Models;

public class ApotekService
{
    private readonly ApotekRepository _apotekRepository;
    private readonly ReceptRepository _receptRepository;
    private readonly ReceptUdleveringRepository _receptUdleveringRepository;
    private readonly OrdinationRepository _ordinationRepository;
    
    public ApotekService(ApotekRepository apotekRepository, ReceptUdleveringRepository receptUdleveringRepository, ReceptRepository receptRepository, OrdinationRepository ordinationRepository)
    { 
        _apotekRepository = apotekRepository;
        _receptRepository = receptRepository;
        _receptUdleveringRepository = receptUdleveringRepository;
        _ordinationRepository = ordinationRepository;
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

    public async Task UdleverOrdination(int ordinationId, int apotekId, int receptId, int antal)
    {
        var recept = _receptRepository.GetById(receptId);
        var apotek = _apotekRepository.GetById(apotekId);
        var ordination = _ordinationRepository.GetById(ordinationId);
        var currentUdleveringer = ordination.FortagneUdleveringer;

        if (antal <= 0)
        {
            throw new ArgumentException("Antallet af udleveringer skal være større end nul.");
        }

        if (currentUdleveringer + antal > ordination.Udleveringer)
        {
            throw new ArgumentException("Antal af ordination er ikke tilladt.");
        }

        if (currentUdleveringer + antal < 0)
        {
            throw new ArgumentException("Antallet af udleveringer kan ikke være negativt.");
        }

        try
        {
            if (currentUdleveringer + antal == ordination.Udleveringer)
            {
                ordination.Lukket = true;
            }

            var receptUdlevering = new ReceptUdlevering
            {
                ApotekId = apotekId,
                ReceptId = receptId,
                Antal = antal,
                UdleveringsDato = DateTime.Now
            };

            if (recept.ReceptUdleveringer == null)
            {
                recept.ReceptUdleveringer = new List<DataAccessLayer.Models.ReceptUdlevering>();
            }
            recept.ReceptUdleveringer.Add(ReceptUdleveringMapper.Map(receptUdlevering));

            if (apotek.ReceptUdleveringer == null)
            {
                apotek.ReceptUdleveringer = new List<DataAccessLayer.Models.ReceptUdlevering>();
            }
            apotek.ReceptUdleveringer.Add(ReceptUdleveringMapper.Map(receptUdlevering));

            ordination.FortagneUdleveringer += antal;

            _ordinationRepository.Update(ordination);
            _receptRepository.Update(recept);
            _apotekRepository.Update(apotek);
        } catch
        {
            throw new Exception("Der skete en fejl ved opdatering af ordination.");
        }
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