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
    internal class PageContactsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<ContactEntry> ContactsEntries { get; set; }

        public PageContactsVM()
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var data = await ApiConnector.Instance.RequestAsync($"contacts/getlist");
                ContactsEntries = JsonConvert.DeserializeObject<List<ContactEntry>>(data);
                OnPropertyChanged("ContactsEntries");
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
