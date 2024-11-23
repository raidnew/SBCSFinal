using API.Context;
using CommonData.Models;

namespace API.Data
{
    public class HeaderTexts : Entries<HeaderText>
    {

        public HeaderTexts(DBContext dBContext) : base(dBContext, dBContext.HeaderTexts) { }

        public override void Edit(HeaderText obj)
        { }

    }
}