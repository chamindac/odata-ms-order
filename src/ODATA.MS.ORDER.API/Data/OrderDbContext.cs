using Microsoft.EntityFrameworkCore;
using ODATA.MS.ORDER.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODATA.MS.ORDER.API.Data
{
    public class OrderDbContext:DbContext
    {
        public OrderDbContext() : base() { }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
        }
    }
}
