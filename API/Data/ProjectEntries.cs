
using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Data
{
    public class ProjectEntries : Entries<ProjectEntry>
    {
        public ProjectEntries(DBContext dBContext) : base(dBContext, dBContext.Projects) {}

        public override void Edit(ProjectEntry obj)
        {
            ProjectEntry editing = GetById(obj.Id);
            editing.Name = obj.Name;
            editing.Description = obj.Description;
            DBContext.SaveChanges();
        }
    }
}
