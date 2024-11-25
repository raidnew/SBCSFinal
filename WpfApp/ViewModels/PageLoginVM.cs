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

        public ICommand ClickLogin
        {
            get
            {
                return _clickLogin ?? (_clickLogin = new CommandHandlerParam((data) => OnClickLoginItem(), () => true));
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

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
