using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace MVCstore.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeNumber { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }

        public int Salary { get; set; }

        public bool mode { get; set; }

        public string EmployeePassword { get; set; }

        
    }
}