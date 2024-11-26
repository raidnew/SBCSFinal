using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Task.Common;
using WebClient.Net;


namespace ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Action<string> OnClickMenu { get; set; }
        public Action OnClickLogin { get; set; }

        public List<API.Models.MenuItem> MenuItems { get; set; }
        public Page WindowContent { get; set; }

        private string _loginBtnText;
        public string LoginBtnText { get => _loginBtnText; set { _loginBtnText = value; OnPropertyChanged("LoginBtnText"); } }

        private Visibility _buttonVisible;
        public Visibility ButtonVisible
        { 
            get => _buttonVisible; 
            set 
            {
                _buttonVisible = value;
                OnPropertyChanged("ButtonVisible");
            }
        }

        private Visibility _buttonVisibleAdmin;
        public Visibility ButtonVisibleAdmin
        { 
            get => _buttonVisible; 
            set 
            {
                _buttonVisible = value;
                OnPropertyChanged("ButtonVisibleAdmin");
            }
        }

        private ICommand _clickMenuItem;
        private ICommand _clickLogin;

        public ICommand ClickLogin
        {
            get
            {
                return _clickLogin ?? (_clickLogin = new CommandHandlerParam((data) => OnClickLoginItem(), () => true));
            }
        }

        public ICommand ClickMenuItem
        {
            get
            {
                return _clickMenuItem ?? (_clickMenuItem = new CommandHandlerParam((data) => OnClickMenuItem(data), () => true));
            }
        }

        public MainWindowVM()
        {
            LoadMenu();
            ApiConnector.Instance.HasLogged += OnLogged;
            OnLogged();
        }

        public void SetContent(Page Content)
        {
            WindowContent = Content;
            OnPropertyChanged("WindowContent");
        }

        private void OnLogged()
        {
            var name = ApiConnector.Instance.GetLoggedName();
            if(name == null)
            {
                LoginBtnText = $"Login";
                ButtonVisible = Visibility.Hidden;
                ButtonVisibleAdmin = Visibility.Hidden;
            }
            else
            {
                LoginBtnText = $"Logout {name}";
                ButtonVisible = Visibility.Visible;
                if(name == "admin")
                    ButtonVisibleAdmin = Visibility.Visible;

            }
            OnClickMenu?.Invoke("");
        }

        /*
        public void ClickOrders()
        {
            OnClickMenu?.Invoke("ORDERS/SHOWALL");
        }

        public void ClickAdmin()
        {
            OnClickMenu?.Invoke("ADMIN/INDEX");
        }
        */

        private void OnClickLoginItem()
        {
            OnClickLogin?.Invoke();
        }

        private void OnClickMenuItem(object link)
        {
            OnClickMenu?.Invoke((string)link);
        }

        private async void LoadMenu()
        {
            try
            {
                var data = await ApiConnector.Instance.RequestAsync($"Header/GetMenu");
                MenuItems = JsonConvert.DeserializeObject<List<API.Models.MenuItem>>(data);
                OnPropertyChanged("MenuItems");
            }
            catch (Exception e)
            {
                var error = e.Message;
                Trace.WriteLine(error);
            }
        }

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
