using System;
using System.Collections.Generic;
using MVCstore.Models;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCstore.DAL
{
    public class EmployeeDAL : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().ToTable("Employees");
        }
        public DbSet<Employee> employees { get; set; }
    }
}