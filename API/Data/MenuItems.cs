using API.Context;
using API.Models;
using CommonData.Models;

namespace API.Data
{
    public class MenuItems : Entries<MenuItem>
    {

        public MenuItems(DBContext dBContext) : base(dBContext, dBContext.Menu) { }

        public override void Edit(MenuItem obj)
        {}

    }
}
