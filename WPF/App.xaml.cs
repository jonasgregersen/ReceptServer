using System.Configuration;
using System.Data;
using System.Windows;
using WPF.Services;
using WPF.Views;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var apiService = new ApiService();
            var loginWindow = new LaegehusLoginWindow(apiService);
            loginWindow.Show();
        }

    }

}
