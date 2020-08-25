using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static LUSS_API.Models.AdjustmentVoucherStatus;

namespace LUSS_API.Models.ViewModels
{
    public class CustomAdjustmentVoucher
    {
        public int AdjustmentID { get; set; }
        public int AdjustQty { get; set; }
        [MaxLength(50)]
        public string AdjustType { get; set; }
        [MaxLength(50)]
        public AdjustmentStatus Status { get; set; }
        public int TotalCost { get; set; }
        public DateTime IssuedDate { get; set; }
        public string VoucherNo { get; set; }
        public string Comment { get; set; }
        public string Reason { get; set; }
        public int ItemID { get; set; }
        public int? ApprovedByID { get; set; }
        public int? RequestByID { get; set; }
        public String RequestedByUser { get; set; }
        public String? ApprovedByUser { get; set; }
        public String ItemCode { get; set; }
        public String CategoryName { get; set; }
        public String ItemName { get; set; }
        public String UOM { get; set; }
        public int ItemPrice { get; set; }
        
        
    }
}
