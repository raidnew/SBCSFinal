using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class DBContext : IdentityDbContext<AppUser>
    {
        public DbSet<Order> Orders { get; set; }
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
    }
}
