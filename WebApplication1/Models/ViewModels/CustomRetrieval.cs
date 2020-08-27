using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models.ViewModels
{
    public class CustomRetrieval
    {
        public int RetrievalID { get; set; }
        public int ReStockLevel { get; set; }
        public int ItemID { get; set; }
        public String ItemCode { get; set; }
        public String ItemName { get; set; }
        public String UOM { get; set; }
        public int ItemPrice { get; set; }
        public String Location { get; set; }
        public int InStockQty { get; set; }
        public String ItemCategory { get; set; }
        public String CategoryName { get; set; }
        public int TotalQty { get; set; }
        public int RequestedQty { get; set; }
        public int AcceptedQty { get; set; }
        public String CollectionDate { get; set; }

    }
}
