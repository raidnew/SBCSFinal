using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using Task.Common;
using WebClient.Net;

namespace ViewModels
{
    public class PageLandingVM : INotifyPropertyChanged
    {

        private ICommand _clickSendOrder;

        public ICommand ClickSendOrder
        {
            get
            {
                return _clickSendOrder ?? (_clickSendOrder = new CommandHandlerParam((data) => SendOrder(), () => true));
            }
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SendOrder()
        {
            Order order = new Order();
            order.Email = Email;
            order.Name = Name;
            order.Message = Message;
            ApiConnector.Instance.RequestAsync("orders/Add", JsonConvert.SerializeObject(order), HttpMethod.Put);
            
        }
    }
}
