using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Task.Common;
using WebClient.Net;


namespace ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<MenuItem> MenuItems { get; set; }

        private ICommand _clickMenuItem;

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
        }

        private void OnClickMenuItem(object link)
        {
            Trace.WriteLine(link);
        }

        private async void LoadMenu()
        {
            BlogEntry test;
            try
            {
                var data = await ApiConnector.Instance.RequestAsync($"Header/GetMenu");
                MenuItems = JsonConvert.DeserializeObject<List<MenuItem>>(data);
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
