using DataAccessLayer.Mappers;
using DataAccessLayer.Interfaces;
using DataTransferLayer.Models;
using DTOApotek = DataTransferLayer.Models.Apotek;
using Business.Interfaces;
using Business.Utils;

namespace Business.Services;

public class ApotekService : IApotekService
{
    private readonly IApotekRepository _apotekRepository;
    private readonly IReceptRepository _receptRepository;
    private readonly IOrdinationRepository _ordinationRepository;
    private readonly IReceptUdleveringRepository _receptUdleveringRepository;

    
    public ApotekService(IApotekRepository apotekRepository, IReceptUdleveringRepository receptUdleveringRepository, IReceptRepository receptRepository, IOrdinationRepository ordinationRepository)
    { 
        _apotekRepository = apotekRepository;
        _receptUdleveringRepository = receptUdleveringRepository;
        _receptRepository = receptRepository;
        _ordinationRepository = ordinationRepository;
    }
    
    public async Task<List<DTOApotek>> GetAll()
    {
        var apoteker = await _apotekRepository.GetAll();
        return apoteker.Select(apotek => ApotekMapper.Map(apotek)).ToList();
    }
    
    public async Task<DTOApotek> GetById(int id)
    {
        var DALapotek = await _apotekRepository.GetById(id);
        return ApotekMapper.Map(DALapotek);
    }

    public async Task UdleverOrdination(int ordinationId, int apotekId, int receptId, int antal)
    {
        var recept = await _receptRepository.GetById(receptId)
            ?? throw new ArgumentException("Recept ikke fundet.");
        var ordination = await _ordinationRepository.GetById(ordinationId)
            ?? throw new ArgumentException("Ordination ikke fundet.");

        if (antal <= 0)
        {
            throw new ArgumentException("Antallet af udleveringer skal være større end nul.");
        }

        if (ordination.FortagneUdleveringer + antal > ordination.Udleveringer)
        {
            throw new ArgumentException("Antal af ordination er ikke tilladt.");
        }

        try
        {

            var receptUdlevering = new ReceptUdlevering
            {
                ApotekId = apotekId,
                ReceptId = receptId,
                Antal = antal,
                UdleveringsDato = DateTime.Now
            };

            await _receptUdleveringRepository.Add(ReceptUdleveringMapper.Map(receptUdlevering));

            ordination.FortagneUdleveringer += antal;
            await _ordinationRepository.Update(ordination);

            var DTORecept = ReceptMapper.Map(recept);
            if (ReceptUtils.AlleOrdinationerUdleveret(DTORecept))
            {
                recept.Lukket = true;
                await _receptRepository.Update(recept);
            }
        } catch (Exception ex)
        {
            throw new Exception("Der skete en fejl ved opdatering af ordination.", ex);
        }
    }
    
    public async Task Add(DTOApotek apotek)
    {
        var DALapotek = ApotekMapper.Map(apotek);
        await _apotekRepository.Add(DALapotek);
    }
    
    public async Task Update(DTOApotek apotek)
    {
        var DALapotek = ApotekMapper.Map(apotek);
        await _apotekRepository.Update(DALapotek);
    }
}