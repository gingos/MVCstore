using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCstore.Models;

namespace MVCstore.ModelView
{
    public class RestockViewModel
    {
        public StockRequest sr { get; set; }
        public List<StockRequest> stockRequestList { get; set; }
    }
}