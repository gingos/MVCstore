using MVCstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCstore.ModelView
{
    public class OrderViewModel
    {
        public Order order { get; set; }
        public List<Order> orderList { get; set; }
    }
}