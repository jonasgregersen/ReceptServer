using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ILægehusRepository
    {
        Task<List<Models.Lægehus>> GetAll();
        Task<Models.Lægehus> GetById(int id);
        Task<Models.Lægehus> GetByYdernummer(string ydernummer);
        Task Add(Models.Lægehus lægehus);
        Task Update(Models.Lægehus lægehus);
        Task Delete(int id);
    }
}
