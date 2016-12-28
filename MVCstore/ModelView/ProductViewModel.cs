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
        public PC pc { get; set; }
        public Printer printer { get; set; }
        public Laptop laptop { get; set; }

        public List<Product> productList { get; set; }
        public List<PC> pcList { get; set; }
        public List<Printer> printerList { get; set; }
        public List<Laptop> laptopList { get; set; }
    }
}