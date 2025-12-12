using System.Windows;
using WPF.Interfaces;

namespace WPF.Views
{
    public partial class LaegehusLoginWindow : Window
    {
        private readonly ViewModels.LoginViewModel _viewModel;
        public LaegehusLoginWindow(IApiService apiService)
        {
            InitializeComponent();

            _viewModel = new ViewModels.LoginViewModel(apiService);
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;
            try
            {                
                await _viewModel.LogIn(YdernummerTextBox.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Fejl", MessageBoxButton.OK, MessageBoxImage.Error);             
            } finally
            {
                LoginButton.IsEnabled = true;
            }
        }
    }
}
