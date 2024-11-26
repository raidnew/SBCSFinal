using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using View;
using ViewModels;
using WebClient.Net;
using WpfApp.Views.Pages;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowVM mvvm;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mv = new MainWindow();
            mvvm = new MainWindowVM();
            mv.DataContext = mvvm;
            mv.Show();

            mvvm.OnClickMenu += ShowContext;
            mvvm.OnClickLogin += ShowLogin;
        }

        private void ShowContext(string link)
        {
            switch (link.ToUpper())
            {
                case "PROJECTS/SHOWALL":
                    ShowPage(new PageProjects(), new PageProjectVM());
                    break;
                case "SERVICES/SHOWALL":
                    ShowPage(new PageServices(), new PageServicesVM());
                    break;
                case "BLOGS/SHOWALL":
                    ShowPage(new PageBlogs(), new PageBlogsVM());
                    break;
                case "CONTACTS/SHOWALL":
                    ShowPage(new PageContacts(), new PageContactsVM());
                    break;
                case "ACCOUNT/LOGIN":
                    ShowLogin();
                    break;
                case "ORDERS/SHOWALL":
                    ShowPage(new PageOrders(), new PageOrdersVM());
                    break;                
                case "ADMIN/INDEX":
                    ShowPage(new PageAdmin(), new PageAdminVM());
                    break;
                case "HOME/INDEX":
                default:
                    ShowPage(new PageLanding(), new PageLandingVM());
                    break;
            }
        }

        private void ShowLogin()
        {
            if (ApiConnector.Instance.GetLoggedName() != null)
                ApiConnector.Instance.Logout();
            else
                ShowPage(new PageLogin(), new PageLoginVM());
        }

        private void ShowPage(Page page, INotifyPropertyChanged viewmodel)
        {
            page.DataContext = viewmodel;
            mvvm.SetContent(page);
        }
    }
}
