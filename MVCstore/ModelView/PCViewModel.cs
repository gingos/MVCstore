using MVCstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCstore.ModelView
{
    public class PCViewModel
    {
        public PC pc { get; set; }
        public List<PC> pcList { get; set; }
    }
}