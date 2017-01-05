using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCstore.Models
{
    public class Product
    {
        [Key]
        public string Model { get; set; }

        public string Maker { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
    }
}