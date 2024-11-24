
using API.Interfaces;

namespace API.Models
{
    public class OrdersStatus : IDbEntity
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
