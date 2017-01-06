using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCstore.Models
{
    public class StockRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReqID { get; set; }

        public string Model { get; set; }
        public int Quantity { get; set; }
        public DateTime? arrivedDate { get; set; }
    }
}