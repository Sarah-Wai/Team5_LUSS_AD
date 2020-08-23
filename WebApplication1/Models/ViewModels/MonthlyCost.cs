using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models.ViewModels
{
    public class MonthlyCost
    {
        public DateTime Date { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int? Sum { get; set; }
        public int? Qty { get; set; }
        public int? TotalPrice { get; set; }
        public int ItemPrice { get; set; }

    }
}
