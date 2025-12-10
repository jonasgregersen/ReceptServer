using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataTransferLayer.Models;
using System.Windows;
using WPF.Services;
using System.Collections.ObjectModel;
using Microsoft.IdentityModel.Tokens;

namespace WPF.ViewModels
{
    public class ReceptViewModel: INotifyPropertyChanged
    {
        private ApiService _apiService;
        private AppState _appState = AppState.Instance;
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

        public ReceptViewModel(ApiService apiService)
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
                ReceptId = Recept.Id
            };
            Recept.Ordinationer.Add(ordination);
        }

        public async Task GemRecept(string cprNummer)
        {
            if (Recept == null)
            {
                MessageBox.Show("Ingen recept at gemme.");
                return;
            }
            if (cprNummer.IsNullOrEmpty())
            {
                MessageBox.Show("Indtast et CPR nummer.");
                return;
            }
            if (Recept.Ordinationer.IsNullOrEmpty())
            {
                MessageBox.Show("Tilføj ordination til recept.");
                return;
            }

            Recept.CprNummer = cprNummer;
            Recept.OprettelsesDato = DateTime.Now;

            try
            {
                bool success = await _apiService.OpretRecept(Recept);
                if (success)
                {
                    MessageBox.Show("Recept gemt!");
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
                MessageBox.Show("Indtast et gyldigt recept ID.");
                return false;
            }
            try
            {
                FundetRecept = await _apiService.GetReceptById(id);
                if (FundetRecept == null)
                {
                    MessageBox.Show("Ingen recept fundet med dette ID.");
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

    public class ReceptViewModelDesign
    {
        // Add properties and logic as needed for design-time support
    }
}
