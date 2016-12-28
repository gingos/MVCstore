using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCstore.Models
{
    public class Laptop
    {
        [Key]
        public string Model { get; set; }

        public int Speed { get; set; }

        public int RAM { get; set; }

        public int HD { get; set; }

        public double Screen { get; set; }

        public int Price { get; set; }
    }
}