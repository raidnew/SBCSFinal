using API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class MenuItem : IDbEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
    }
}
