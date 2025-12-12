using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IOrdinationRepository
    {
        Task<List<Ordination>> GetAll();
        Task<Ordination> GetById(int id);
        Task Add(Ordination ordination);
        Task Update(Ordination ordination);
    }
}
