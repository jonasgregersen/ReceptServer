using DTOOrdination = DataTransferLayer.Models.Ordination;

namespace Business.Interfaces
{
    public interface IOrdinationService
    {
        Task<List<DTOOrdination>> GetAll();
        Task<DTOOrdination> GetById(int id);
        Task Add(DTOOrdination ordination);
        Task Update(DTOOrdination ordination);
    }
}
