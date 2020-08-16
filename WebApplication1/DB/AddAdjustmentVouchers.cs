using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.DB
{
    public class AddAdjustmentVouchers
    {
        public static List<AdjustmentVoucher> getAllAdjustmentVoucher()
        {
            List<AdjustmentVoucher> adjustmentVouchers = new List<AdjustmentVoucher>();

            AdjustmentVoucher adj1 = new AdjustmentVoucher();
            adj1.AdjustmentID = 1;
            adj1.AdjustQty = 5;
            adj1.AdjustType = "Deduct";
            adj1.Status = AdjustmentVoucherStatus.AdjustmentStatus.Approved;
            adj1.TotalCost = 25;
            adj1.IssuedDate = DateTime.Now;
            adj1.VoucherNo = "0001";
            adj1.Comment = "None";
            adj1.Reason = "None";
            adj1.ItemID = 1;
            adj1.RequestByID = 1;
            adj1.ApprovedByID = 2;

            AdjustmentVoucher adj2 = new AdjustmentVoucher();
            adj2.AdjustmentID = 2;
            adj2.AdjustQty = 4;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Approved;
            adj2.TotalCost = 20;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0002";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 2;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            adjustmentVouchers.Add(adj1);
            adjustmentVouchers.Add(adj2);
            return adjustmentVouchers;
        }
    }
}
