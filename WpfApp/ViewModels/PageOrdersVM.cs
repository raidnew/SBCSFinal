using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebClient.Net;

namespace ViewModels
{
    internal class PageOrdersVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Order> OrdersEntries { get; set; }

        public PageOrdersVM()
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var data = await ApiConnector.Instance.RequestAsync($"orders/getlist");
                OrdersEntries = JsonConvert.DeserializeObject<List<Order>>(data);
                OnPropertyChanged("OrdersEntries");
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
