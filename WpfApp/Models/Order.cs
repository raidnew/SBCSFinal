using API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Order : IDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string Message { get; set; }
        public int StatusId { get; set; }
    }
}
