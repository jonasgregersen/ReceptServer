using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories;
using DataTransferLayer.Models;
using DTOApotek = DataTransferLayer.Models.Apotek;

namespace Business.Services;

public class ApotekService
{
    private readonly ApotekRepository _apotekRepository;
    private readonly ReceptRepository _receptRepository;
    private readonly OrdinationRepository _ordinationRepository;
    private readonly ReceptUdleveringRepository _receptUdleveringRepository;

    
    public ApotekService(ApotekRepository apotekRepository, ReceptUdleveringRepository receptUdleveringRepository, ReceptRepository receptRepository, OrdinationRepository ordinationRepository)
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
            if (ReceptService.AlleOrdinationerUdleveret(DTORecept))
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