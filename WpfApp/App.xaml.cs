using System.Windows;
using View;
using ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindowVM mwvm = new MainWindowVM();
            MainWindow mv = new MainWindow();
            mv.DataContext = mwvm;
            mv.Show();
        }
    }
}
