using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task.Common;
using WebClient.Auth;
using WebClient.Net;

namespace ViewModels
{
    internal class PageLoginVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand _clickLogin;
        private ICommand _clickLoginUser;
        private ICommand _clickLoginAdmin;

        public ICommand ClickLogin
        {
            get
            {
                return _clickLogin ?? (_clickLogin = new CommandHandlerParam((data) => OnClickLoginItem(), () => true));
            }
        }

        public ICommand ClickLoginUser
        {
            get
            {
                return _clickLoginUser ?? (_clickLoginUser = new CommandHandlerParam((data) => OnClickLoginItemPrefill("user", "1234"), () => true));
            }
        }

        public ICommand ClickLoginAdmin
        {
            get
            {
                return _clickLoginAdmin ?? (_clickLoginAdmin = new CommandHandlerParam((data) => OnClickLoginItemPrefill("admin", "1234"), () => true));
            }
        }


        public string Login { get; set; }
        public string Password{ get; set; }

        public PageLoginVM()
        {
            
        }

        private void OnClickLoginItem()
        {
            AuthenticationRequest loginRequest = new AuthenticationRequest();
            loginRequest.Name = Login;
            loginRequest.Password = Password;
            ApiConnector.Instance.AuthRequest(loginRequest);
        }
        private void OnClickLoginItemPrefill(string login, string password)
        {
            AuthenticationRequest loginRequest = new AuthenticationRequest();
            loginRequest.Name = login;
            loginRequest.Password = password;
            ApiConnector.Instance.AuthRequest(loginRequest);
        }

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
