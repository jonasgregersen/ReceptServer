using System.ComponentModel;
using System.Windows;
using WPF.Views;
using WPF.Interfaces;

namespace WPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IApiService _apiService;
        public LoginViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        private string _ydernummer;
        public string Ydernummer
        {
            get { return _ydernummer; }
            set
            {
                _ydernummer = value;
                OnPropertyChanged(nameof(Ydernummer));
            }
        }
        public async Task LogIn(string ydernummer)
        {   
            var lægehus = await _apiService.GetLægehusByYdernummer(ydernummer);
            
            if (lægehus == null)
            {
                throw new Exception("Lægehus ikke fundet!");
            }
            
            Services.AppState.Instance.SetLægehus(lægehus);

            var main = new MainWindow(_apiService);
            main.Show();

            Application.Current.Windows
                .OfType<LaegehusLoginWindow>()
                .First()
                .Close();                             
        }

        public async Task LogOut()
        {
            if (Services.AppState.Instance.CurrentLægehus != null)
            {
                Services.AppState.Instance.SetLægehus(null!);

                var login = new LaegehusLoginWindow(_apiService);
                login.Show();

                Application.Current.Windows
                    .OfType<MainWindow>()
                    .First()
                    .Close();
            }
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
