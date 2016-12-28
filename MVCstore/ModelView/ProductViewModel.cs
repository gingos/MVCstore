using MVCstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCstore.ModelView
{
    public class ProductViewModel
    {
        public Product product { get; set; }

        public List<Product> productList { get; set; }
    }
}