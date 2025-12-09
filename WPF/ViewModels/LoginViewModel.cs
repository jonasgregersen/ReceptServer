using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.Views;

namespace WPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly WPF.Services.ApiService _apiService;
        public LoginViewModel()
        {
            _apiService = new WPF.Services.ApiService();
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

            var main = new WPF.MainWindow();
            main.Show();

            Application.Current.Windows
                .OfType<LaegehusLoginWindow>()
                .First()
                .Close();                             
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
