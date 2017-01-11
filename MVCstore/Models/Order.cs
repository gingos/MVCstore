﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCstore.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdID { get; set; }

        public int CustID { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime? shippedDate { get; set; }
    }
}