using DTOApotek = DataTransferLayer.Models.Apotek;

namespace Business.Interfaces
{
    public interface IApotekService
    {
        Task<List<DTOApotek>> GetAll();
        Task<DTOApotek> GetById(int id);
        Task UdleverOrdination(int ordinationId, int apotekId, int receptId, int antal);
        Task Add(DTOApotek apotek);
        Task Update(DTOApotek apotek);
    }
}
