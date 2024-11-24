using API.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Task.Common;

namespace API.Models
{
    public class MenuItem : IDbEntity
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }

        private ICommand _clickMenuItem;

        public ICommand ClickMenuItem
        {
            get
            {
                return _clickMenuItem ?? (_clickMenuItem = new CommandHandler(OnClickMenuItem2, () => true));
            }
        }
        private void OnClickMenuItem2()
        {
            Trace.WriteLine("OnClickMenuItem2");
        }
    }
}
