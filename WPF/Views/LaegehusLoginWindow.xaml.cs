using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Views
{
    public partial class LaegehusLoginWindow : Window
    {
        private readonly WPF.ViewModels.LoginViewModel _viewModel = new WPF.ViewModels.LoginViewModel();
        public LaegehusLoginWindow()
        {
            InitializeComponent();
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
