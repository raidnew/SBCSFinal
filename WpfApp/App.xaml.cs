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
                    ShowProjects();
                    break;
                case "SERVICES/SHOWALL":
                    ShowServices();
                    break;
                case "BLOGS/SHOWALL":
                    ShowBlogs();
                    break;
                case "CONTACTS/SHOWALL":
                    ShowContacts();
                    break;
                case "ACCOUNT/LOGIN":
                    ShowLogin();
                    break;
                case "ORDERS/SHOWALL":
                    ShowOrders();
                    break;                
                case "ADMIN/INDEX":
                    break;
                case "HOME/INDEX":
                    ShowLanding();
                    break;
                default:
                    ShowLanding();
                    break;
            }
        }

        private void ShowLanding()
        {
            Page page = new PageLanding();
            PageLandingVM vm = new PageLandingVM();
            page.DataContext = vm;
            mvvm.SetContent(page);
        }

        private void ShowServices()
        {
            Page page = new PageServices();
            PageServicesVM vm = new PageServicesVM();
            page.DataContext = vm;
            mvvm.SetContent(page);
        }

        private void ShowBlogs()
        {
            Page page = new PageBlogs();
            PageBlogsVM vm = new PageBlogsVM();
            page.DataContext = vm;
            mvvm.SetContent(page);
        }

        private void ShowOrders()
        {
            Page page = new PageOrders();
            PageOrdersVM vm = new PageOrdersVM();
            page.DataContext = vm;
            mvvm.SetContent(page);
        }

        private void ShowContacts()
        {
            Page page = new PageContacts();
            PageContactsVM vm = new PageContactsVM();
            page.DataContext = vm;
            mvvm.SetContent(page);
        }

        private void ShowProjects()
        {
            Page page = new PageProjects();
            PageProjectVM vm = new PageProjectVM();
            page.DataContext = vm;
            mvvm.SetContent(page);
        }

        private void ShowLogin()
        {
            if (ApiConnector.Instance.GetLoggedName() != null)
            {
                ApiConnector.Instance.Logout();
            }
            else
            {
                Page page = new PageLogin();
                PageLoginVM vm = new PageLoginVM();
                page.DataContext = vm;
                mvvm.SetContent(page);
            }
        }
    }
}
