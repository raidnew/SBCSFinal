using API.Context;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Data
{
    public class ContactsEntries : Entries<ContactEntry>
    {
        public ContactsEntries(DBContext dBContext) : base(dBContext, dBContext.Contacts) {}

        [Authorize]
        public override void Edit(ContactEntry obj)
        {
            ContactEntry editingOrder = GetById(obj.Id);
            editingOrder.Name = obj.Name;
            editingOrder.Address = obj.Address;
            DBContext.SaveChanges();
        }
    }
}
