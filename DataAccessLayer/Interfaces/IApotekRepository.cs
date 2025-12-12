using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IApotekRepository
    {
        Task<List<Apotek>> GetAll();
        Task<Apotek> GetById(int id);
        Task Add(Apotek apotek);
        Task Update(Apotek apotek);
    }
}
