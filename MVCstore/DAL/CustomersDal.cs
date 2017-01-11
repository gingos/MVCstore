using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCstore.Models;

namespace MVCstore.DAL
{
    public class CustomersDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customers>().ToTable("Customers");
        }
        public DbSet<Customers> customers { get; set; }
    }
}