using WPF.Models;

namespace WPF.Services
{
    public class AppState
    {
        private static AppState _instance;
        public static AppState Instance => _instance ??= new AppState();
        public Lægehus? CurrentLægehus { get; private set; }
        private AppState() { }
        public void SetLægehus(Lægehus lægehus)
        {
            CurrentLægehus = lægehus;
        }
    }

}
