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

            AdjustmentVoucher adj3 = new AdjustmentVoucher();
            adj2.AdjustQty = 2;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Approved;
            adj2.TotalCost = 50;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0003";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 1;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj4 = new AdjustmentVoucher();
            adj2.AdjustQty = 10;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Approved;
            adj2.TotalCost = 40;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0004";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 1;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj5 = new AdjustmentVoucher();
            adj2.AdjustQty = 1;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Approved;
            adj2.TotalCost = 12;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0005";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 1;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj6 = new AdjustmentVoucher();
            adj2.AdjustQty = 30;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Approved;
            adj2.TotalCost = 40;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0006";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 14;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj7 = new AdjustmentVoucher();
            adj2.AdjustQty = 30;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 100;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0007";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 16;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj8 = new AdjustmentVoucher();
            adj2.AdjustQty = 48;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 60;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0008";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 21;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj9 = new AdjustmentVoucher();
            adj2.AdjustQty = 20;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 400;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0009";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 19;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj10 = new AdjustmentVoucher();
            adj2.AdjustQty = 40;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 30;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0010";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 40;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj11 = new AdjustmentVoucher();
            adj2.AdjustQty = 40;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 500;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0011";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 16;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj12 = new AdjustmentVoucher();
            adj2.AdjustQty = 9;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 640;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0012";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 11;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj13 = new AdjustmentVoucher();
            adj2.AdjustQty = 10;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 200;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0013";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 3;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj14 = new AdjustmentVoucher();
            adj2.AdjustQty = 10;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 400;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0014";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 1;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj15 = new AdjustmentVoucher();
            adj2.AdjustQty = 14;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 400;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0015";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 1;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj16 = new AdjustmentVoucher();
            adj2.AdjustQty = 10;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 400;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0016";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 1;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            adjustmentVouchers.Add(adj1);
            adjustmentVouchers.Add(adj2);
            adjustmentVouchers.Add(adj3);
            adjustmentVouchers.Add(adj4);
            adjustmentVouchers.Add(adj5);
            adjustmentVouchers.Add(adj6);
            adjustmentVouchers.Add(adj7);
            adjustmentVouchers.Add(adj8);
            adjustmentVouchers.Add(adj9);
            adjustmentVouchers.Add(adj10);
            adjustmentVouchers.Add(adj11);
            adjustmentVouchers.Add(adj12);
            adjustmentVouchers.Add(adj13);
            adjustmentVouchers.Add(adj14);
            adjustmentVouchers.Add(adj15);
            adjustmentVouchers.Add(adj16);
            return adjustmentVouchers;
        }
    }
}
