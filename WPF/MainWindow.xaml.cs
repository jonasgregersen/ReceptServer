using System.Windows;
using WPF.ViewModels;
using WPF.Services;
using Microsoft.IdentityModel.Tokens;

namespace WPF
{
    public partial class MainWindow : Window
    {
        private readonly ReceptViewModel _receptViewModel;
        private readonly LoginViewModel _loginViewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Opret ViewModel og sæt DataContext
            _receptViewModel = new ReceptViewModel(new ApiService());
            _loginViewModel = new LoginViewModel();
            DataContext = _receptViewModel;

            Loaded += async (_, __) => await _receptViewModel.HentLægehusRecepter();
        }

        private void TilfoejOrdination_Click(object sender, RoutedEventArgs e)
        {
            _receptViewModel.TilføjOrdination();
        }

        private void GemRecept_Click(object sender, RoutedEventArgs e)
        {
            var cprNummer = CprTextBox.Text.Trim();
            _receptViewModel.GemRecept(cprNummer);
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
                MessageBox.Show("Indtast venligst et gyldigt Recept ID.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void LogOut_Click(object sender, RoutedEventArgs e)
        {
            await _loginViewModel.LogOut();
        }
    }
}
