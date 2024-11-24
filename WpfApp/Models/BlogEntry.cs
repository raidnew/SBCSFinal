using API.Interfaces;
using System;

namespace API.Models
{
    public class BlogEntry : IDbEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
