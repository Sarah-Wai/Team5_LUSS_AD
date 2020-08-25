using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models.ViewModels
{
    public class CustomItem
    {
        public int ItemID { get; set; }
        public String ItemCode { get; set; }
        public String ItemName { get; set; }
        public String Location { get; set; }
        public String UOM { get; set; }
        public int InStockQty { get; set; }
        public int ReOrderLevel { get; set; }
        public int ReOrderQty { get; set; }
        public String CategoryName { get; set; }
    }
}
