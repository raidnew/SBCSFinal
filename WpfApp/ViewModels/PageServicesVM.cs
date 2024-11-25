using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WebClient.Net;

namespace ViewModels
{
    public class PageServicesVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<ServiceEntry> ServicesEntries { get; set; }

        public PageServicesVM()
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var data = await ApiConnector.Instance.RequestAsync($"services/getlist");
                ServicesEntries = JsonConvert.DeserializeObject<List<ServiceEntry>>(data);
                OnPropertyChanged("ServicesEntries");
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
