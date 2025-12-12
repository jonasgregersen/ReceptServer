using DTOLægehus = DataTransferLayer.Models.Lægehus;

namespace Business.Interfaces
{
    public interface ILægehusService
    {
        Task<List<DTOLægehus>> GetAll();

        Task<DTOLægehus> GetById(int id);

        Task<DTOLægehus> GetByYdernummer(string ydernummer);

        Task Add(DTOLægehus lægehus);

        Task Update(DTOLægehus lægehus);
    }
}
