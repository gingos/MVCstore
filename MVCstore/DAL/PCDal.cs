using MVCstore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCstore.DAL
{
    public class PCDal : DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PC>().ToTable("PC");
        }
        public DbSet<PC> PCs { get; set; }
    }
}