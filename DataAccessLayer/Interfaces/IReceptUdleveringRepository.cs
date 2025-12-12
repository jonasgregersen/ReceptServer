using DataAccessLayer.Models;


namespace DataAccessLayer.Interfaces
{
    public interface IReceptUdleveringRepository
    {
        Task Add(ReceptUdlevering receptUdlevering);
    }
}
