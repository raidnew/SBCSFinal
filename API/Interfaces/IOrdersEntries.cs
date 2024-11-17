using API.Models;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IOrdersEntries
    {
        public IEnumerable<Order> GetOrders();
        public Order GetOrderById(int id);
        public void EditOrder(Order order);
        public void AddOrder(Order order);
        public void RemoveOrder(int id);
    }
}
