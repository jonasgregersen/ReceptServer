using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface ILægehusRepository
    {
        Task<List<Lægehus>> GetAll();
        Task<Lægehus> GetById(int id);
        Task<Lægehus> GetByYdernummer(string ydernummer);
        Task Add(Lægehus lægehus);
        Task Update(Lægehus lægehus);
        Task Delete(int id);
    }
}
