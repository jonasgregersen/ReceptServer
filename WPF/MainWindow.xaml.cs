using System.Windows;
using WPF.ViewModels;
using WPF.Models;
using WPF.Services;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Controls;
using WPF.Interfaces;

namespace WPF
{
    public partial class MainWindow : Window
    {
        private readonly ReceptViewModel _receptViewModel;
        private readonly LoginViewModel _loginViewModel;

        public MainWindow(IApiService apiService)
        {
            InitializeComponent();

            _receptViewModel = new ReceptViewModel(apiService);
            _loginViewModel = new LoginViewModel(apiService);
            DataContext = _receptViewModel;

            Loaded += async (_, __) => await _receptViewModel.HentLægehusRecepter();
        }

        private void TilfoejOrdination_Click(object sender, RoutedEventArgs e)
        {
            _receptViewModel.TilføjOrdination();
        }

        private void GemRecept_Click(object sender, RoutedEventArgs e)
        {
            if (HarValidationFejl())
            {
                MessageBox.Show("Der er valideringsfejl i ordinationerne.\nRet dem før recepten kan gemmes.",
                        "Valideringsfejl",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                return;
            }
            var cprNummer = CprTextBox.Text.Trim();
            _receptViewModel.GemRecept(cprNummer);
        }
        private bool HarValidationFejl()
        {
            foreach (var o in _receptViewModel.Recept.Ordinationer)
            {
                if (!string.IsNullOrEmpty(o[nameof(o.Lægemiddel)]) ||
                    !string.IsNullOrEmpty(o[nameof(o.Dosis)]) ||
                    !string.IsNullOrEmpty(o[nameof(o.Udleveringer)]) ||
                    !string.IsNullOrEmpty(o[nameof(o.FortagneUdleveringer)]))
                {
                    return true;
                }
            }

            return false;
        }

        private void SletOrdination_Click(object sender, RoutedEventArgs e)
        {
            if (NyOrdinationerGrid.SelectedItem is Ordination selectedOrdination)
            {
                _receptViewModel.SletOrdination(selectedOrdination);
            }
            else
            {
                MessageBox.Show("Vælg venligst en ordination at slette.", "Ingen Valg", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Nulstil_Click(object sender, RoutedEventArgs e)
        {
            _receptViewModel.InitRecept();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _receptViewModel.HentLægehusRecepter();
        }

        private async void SøgRecept_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ReceptIdTextBox.Text.Trim(), out int receptId))
            {
                if (!(await _receptViewModel.SøgRecept(receptId))) return;

                OrdinationerGrid.Items.Clear();
                foreach (var item in _receptViewModel.FundetRecept.Ordinationer)
                {
                    OrdinationerGrid.Items.Add(item);
                }
                CprText.Text = _receptViewModel.FundetRecept.CprNummer;
                YderNummerText.Text = _receptViewModel.FundetRecept.YderNummer;
            }
            else
            {
                MessageBox.Show("Indtast venligst et gyldigt Recept ID.", "Ugyldigt ID", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private async void LogOut_Click(object sender, RoutedEventArgs e)
        {
            await _loginViewModel.LogOut();
        }
    }
}
