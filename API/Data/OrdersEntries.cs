using API.Context;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class OrdersEntries : Entries<Order>
    {
        private DBContext _dbContext;

        public OrdersEntries(DBContext dBContext) : base(dBContext, dBContext.Orders)
        {
            _dbContext = dBContext;
        }

        public override void Edit(Order order)
        {
            Order editingOrder = GetById(order.Id);
            editingOrder.Name = order.Name;
            editingOrder.Email = order.Email;
            editingOrder.Message = order.Message;
            editingOrder.StatusId = order.StatusId;
            _dbContext.SaveChanges();
        }

    }
}
