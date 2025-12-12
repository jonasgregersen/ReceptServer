using System.ComponentModel;
using WPF.Models;
using System.Windows;
using WPF.Services;
using System.Collections.ObjectModel;
using Microsoft.IdentityModel.Tokens;
using WPF.Interfaces;

namespace WPF.ViewModels
{
    public class ReceptViewModel: INotifyPropertyChanged
    {
        private readonly IApiService _apiService;
        private readonly AppState _appState = AppState.Instance;
        private Recept _recept;
        private Recept _fundetRecept;
        private string _lægehusNavn;
        public ObservableCollection<Recept> LægehusRecepter { get; set; }
        public Recept Recept
        {
            get { return _recept; }
            set
            {
                _recept = value;
                OnPropertyChanged(nameof(Recept));
            }
        }
        public Recept FundetRecept
        {
            get { return _fundetRecept; }
            set
            {
                _fundetRecept = value;
                OnPropertyChanged(nameof(FundetRecept));
            }
        }
        public string LægehusNavn
        {
            get { return _lægehusNavn; }
            set
            {
                _lægehusNavn = value;
                OnPropertyChanged(nameof(LægehusNavn));
            }
        }

        public ReceptViewModel(IApiService apiService)
        {
            _apiService = apiService;
            InitRecept();
            LægehusNavn = _appState.CurrentLægehus.Navn;
        }
        public async Task HentLægehusRecepter()
        {
            try
            {
                LægehusRecepter = await _apiService.GetLægehusRecepter(_appState.CurrentLægehus.Id);
                OnPropertyChanged(nameof(LægehusRecepter));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved hentning af recepter: " + ex.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TilføjOrdination()
        {
            var ordination = new Ordination
            {
                Lægemiddel = "",
                Dosis = "",
                Udleveringer = 0,
                FortagneUdleveringer = 0,
                ReceptId = Recept.Id
            };
            Recept.Ordinationer.Add(ordination);
        }

        public void SletOrdination(Ordination ordination)
        {
            if (Recept.Ordinationer.Contains(ordination))
            {
                Recept.Ordinationer.Remove(ordination);
            }
        }

        public async Task GemRecept(string cprNummer)
        {
            if (Recept == null)
            {
                MessageBox.Show("Ingen recept at gemme.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (cprNummer.IsNullOrEmpty())
            {
                MessageBox.Show("Indtast et CPR nummer.", "Advarsel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Recept.Ordinationer.IsNullOrEmpty())
            {
                MessageBox.Show("Tilføj ordination til recept.", "Advarsel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Recept.CprNummer = cprNummer;
            Recept.OprettelsesDato = DateTime.Now;

            try
            {
                bool success = await _apiService.OpretRecept(Recept);
                if (success)
                {
                    MessageBox.Show("Recept gemt!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved oprettelse af recept: " + ex.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            } finally
            {
                // Reset recept after saving
                InitRecept();
                await HentLægehusRecepter();
            }
        }

        public async Task<bool> SøgRecept(int id)
        {
            if (id <= 0)
            {
                MessageBox.Show("Indtast venligst et gyldigt recept ID.", "Ugyldigt ID", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            try
            {
                FundetRecept = await _apiService.GetReceptById(id);
                if (FundetRecept == null)
                {
                    MessageBox.Show("Ingen recept fundet med dette ID.", "Recept ikke fundet", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved søgning af recept: " + ex.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public void InitRecept()
        {
            Recept = new Recept
            {
                YderNummer = _appState.CurrentLægehus.YderNummer,
                LægehusId = _appState.CurrentLægehus.Id,
                Ordinationer = new ObservableCollection<Ordination>()
            };
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
