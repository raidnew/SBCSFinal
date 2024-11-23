using API.Context;
using API.Models;

namespace API.Data
{
    public class ServicesEntries : Entries<ServiceEntry>
    {
        public ServicesEntries(DBContext dBContext) : base(dBContext, dBContext.Services) { }
        public override void Edit(ServiceEntry obj)
        {
            ServiceEntry editingOrder = GetById(obj.Id);
            editingOrder.Name = obj.Name;
            editingOrder.Description = obj.Description;
            DBContext.SaveChanges();
        }
    }
}
