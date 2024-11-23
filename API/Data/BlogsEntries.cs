using API.Context;
using API.Models;
using CommonData.Models;

namespace API.Data
{
    public class BlogsEntries : Entries<BlogEntry>
    {
        public BlogsEntries(DBContext dBContext) : base(dBContext, dBContext.Blogs) { }
        public override void Edit(BlogEntry obj)
        {
            BlogEntry editingOrder = GetById(obj.Id);
            editingOrder.Name = obj.Name;
            editingOrder.ShortDescription = obj.ShortDescription;
            editingOrder.Description = obj.Description;
            editingOrder.Date = obj.Date;
            DBContext.SaveChanges();
        }
    }
}
