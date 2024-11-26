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
    public class PageAdminVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        List<Order> _ordersEntries;
        public List<Order> OrdersEntries { get => _ordersEntries; set
            {
                _ordersEntries = value;
                OnPropertyChanged("OrdersEntries");
            }
        }

        public List<BlogEntry> _blogsEntries;
        public List<BlogEntry> BlogsEntries { get => _blogsEntries; set 
            {
                _blogsEntries = value;
                OnPropertyChanged("BlogsEntries");
            }
        }

        public List<ContactEntry> _contactsEntries;
        public List<ContactEntry> ContactsEntries { get => _contactsEntries; set 
            {
                _contactsEntries = value;
                OnPropertyChanged("ContactsEntries");
            }
        }

        public List<ProjectEntry> _projectEntries;
        public List<ProjectEntry> ProjectEntries { get => _projectEntries; set 
            {
                _projectEntries = value;
                OnPropertyChanged("ProjectEntries");
            }
        }

        public PageAdminVM()
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                string data;
                data = await ApiConnector.Instance.RequestAsync($"projects/getlist");
                ProjectEntries = JsonConvert.DeserializeObject<List<ProjectEntry>>(data);
                data = await ApiConnector.Instance.RequestAsync($"blogs/getlist");
                BlogsEntries = JsonConvert.DeserializeObject<List<BlogEntry>>(data);
                data = await ApiConnector.Instance.RequestAsync($"contacts/getlist");
                ContactsEntries = JsonConvert.DeserializeObject<List<ContactEntry>>(data);
                data = await ApiConnector.Instance.RequestAsync($"orders/getlist");
                OrdersEntries = JsonConvert.DeserializeObject<List<Order>>(data);
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
