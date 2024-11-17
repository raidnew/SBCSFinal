using API.Context;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class OrdersEntries : IOrdersEntries
    {
        private DBContext _dbContext;

        public OrdersEntries(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }

        public void EditOrder(Order order)
        {
            Order editingOrder = GetOrderById(order.Id);
            editingOrder.Name = order.Name;
            editingOrder.Email = order.Email;
            editingOrder.Message = order.Message;
            editingOrder.StatusId = order.StatusId;
            _dbContext.SaveChanges();
        }

        public Order GetOrderById(int id)
        {
            return _dbContext.Orders.First<Order>(o => o.Id == id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _dbContext.Orders;
        }

        public void RemoveOrder(int id)
        {
            _dbContext.Orders.Remove(GetOrderById(id));
            _dbContext.SaveChanges();
        }
    }
}
