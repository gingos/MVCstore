using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCstore.Models
{
    public class Product
    {
        public string Maker { get; set; }

        [Key]
        public string Model { get; set; }

        public string Type { get; set; }
    }
}