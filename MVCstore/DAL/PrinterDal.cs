using MVCstore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCstore.DAL
{
    public class PrinterDal : DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Printer>().ToTable("Printer");
        }
        public DbSet<Printer> printers { get; set; }
    }
}