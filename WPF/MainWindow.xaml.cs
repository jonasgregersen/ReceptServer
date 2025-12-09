using System.Windows;
using WPF.ViewModels;
using WPF.Services;
using Microsoft.IdentityModel.Tokens;

namespace WPF
{
    public partial class MainWindow : Window
    {
        private readonly ReceptViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Opret ViewModel og sæt DataContext
            _viewModel = new ReceptViewModel(new ApiService());
            DataContext = _viewModel;

            Loaded += async (_, __) => await _viewModel.HentLægehusRecepter();
        }

        private void TilfoejOrdination_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.TilføjOrdination();
        }

        private void GemRecept_Click(object sender, RoutedEventArgs e)
        {
            var cprNummer = CprTextBox.Text.Trim();
            _viewModel.GemRecept(cprNummer);
        }
        private async void SøgRecept_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ReceptIdTextBox.Text.Trim(), out int receptId))
            {
                await _viewModel.SøgRecept(receptId);
                OrdinationerGrid.Items.Clear();
                foreach (var item in _viewModel.FundetRecept.Ordinationer)
                {
                    OrdinationerGrid.Items.Add(item);
                }
                CprText.Text = _viewModel.FundetRecept.CprNummer;
                YderNummerText.Text = _viewModel.FundetRecept.YderNummer;
            }
            else
            {
                MessageBox.Show("Indtast venligst et gyldigt Recept ID.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
