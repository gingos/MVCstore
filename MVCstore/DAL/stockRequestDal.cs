using MVCstore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCstore.DAL
{
    public class stockRequestDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StockRequest>().ToTable("stockOrders");
        }
        public DbSet<StockRequest> stockRequests { get; set; }
    }
}