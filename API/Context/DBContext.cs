﻿using API.Models;
using CommonData.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class DBContext : IdentityDbContext<AppUser>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProjectEntry> Projects { get; set; }
        public DbSet<BlogEntry> Blogs { get; set; }
        public DbSet<ServiceEntry> Services { get; set; }
        public DbSet<ContactEntry> Contacts { get; set; }
        public DbSet<OrdersStatus> OrdersStatuses { get; set; }
        public DbSet<MenuItem> Menu { get; set; }
        public DbSet<HeaderText> HeaderTexts { get; set; }
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
    }
}
