using DTORecept = DataTransferLayer.Models.Recept;

namespace Business.Interfaces
{
    public interface IReceptService
    {
        Task<List<DTORecept>> GetByCpr(string cprNummer);
        Task<DTORecept> GetById(int id);
        Task<List<DTORecept>> GetAll();
        Task<List<DTORecept>> GetByLægehusId(int id);
        Task Add(DTORecept recept);
        Task Update(DTORecept recept);
    }
}
