using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCstore.Models
{
    public class Printer
    {
        [Key]
        public string Model { get; set; }

        public string Color { get; set; }

        public string Type { get; set; }

        public int Price { get; set; }
    }
}