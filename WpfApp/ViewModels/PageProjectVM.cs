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
    public class PageProjectVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<ProjectEntry> ProjectEntries { get; set; }

        public PageProjectVM()
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var data = await ApiConnector.Instance.RequestAsync($"projects/getlist");
                ProjectEntries = JsonConvert.DeserializeObject<List<ProjectEntry>>(data);
                OnPropertyChanged("ProjectEntries");
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
