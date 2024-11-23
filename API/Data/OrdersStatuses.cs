using API.Context;
using API.Models;

namespace API.Data
{
    public class OrdersStatuses : Entries<OrdersStatus>
    {
        public OrdersStatuses(DBContext dBContext) : base(dBContext, dBContext.OrdersStatuses) {}

        public override void Edit(OrdersStatus obj)
        {
        }
    }
}
