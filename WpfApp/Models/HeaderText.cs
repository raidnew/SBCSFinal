using API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData.Models
{
    public class HeaderText : IDbEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
