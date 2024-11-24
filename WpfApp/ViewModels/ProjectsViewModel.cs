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

namespace ViewModels;

public class ProjectsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public List<ProjectEntry> ProjectEntries { get; set; }

    private async void LoadMenu()
    {
        try
        {
            var data = await ApiConnector.Instance.RequestAsync($"");
            ProjectEntries = JsonConvert.DeserializeObject<List<ProjectEntry>>(data);
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
