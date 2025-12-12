using System.Collections.ObjectModel;
using WPF.Models;

namespace WPF.Interfaces
{
    public interface IApiService
    {
        Task<Lægehus?> GetLægehusByYdernummer(string ydernummer);
        Task<Recept?> GetReceptById(int id);
        Task<bool> OpretRecept(Recept recept);
        Task<ObservableCollection<Recept>> GetLægehusRecepter(int id);
    }
}
