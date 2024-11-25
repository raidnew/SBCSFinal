using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class PageAdminVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public List<ContactEntry> ContactsEntries { get; set; }

        public PageAdminVM()
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                /*
                var data = await ApiConnector.Instance.RequestAsync($"contacts/getlist");
                ContactsEntries = JsonConvert.DeserializeObject<List<ContactEntry>>(data);
                OnPropertyChanged("ContactsEntries");
                */
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
