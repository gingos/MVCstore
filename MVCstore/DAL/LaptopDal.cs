using MVCstore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCstore.DAL
{
    public class LaptopDal : DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Laptop>().ToTable("Laptop");
        }
        public DbSet<Laptop> laptops { get; set; }
    }
}