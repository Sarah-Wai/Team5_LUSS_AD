using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models.ViewModels
{
    public class CustomRequestDetail
    {
        public int RequestDetailID { get; set; }
        public int RequestQty { get; set; }
        public int ItemID { get; set; }
        public int RequestID { get; set; }
        public int? FulfillQty { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UOM { get; set; }
        public int inStockQty { get; set; }
        public int? ReceivedQty { get; set; }


    }
}
