using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IReceptRepository
    {
        Task<List<Recept>> GetByCpr(string cprNummer);
        Task<List<Recept>> GetAll();
        Task<List<Recept>> GetByLægehusId(int id);
        Task<Recept?> GetById(int id);
        Task Add(Recept recept);
        Task Update(Recept recept);
     
    }
}
