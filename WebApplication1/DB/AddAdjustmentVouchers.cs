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
            adj1.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
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
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 20;
            adj2.IssuedDate = DateTime.Now;
            adj2.VoucherNo = "0002";
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = 2;
            adj2.RequestByID = 1;
            adj2.ApprovedByID = 2;

            AdjustmentVoucher adj3 = new AdjustmentVoucher();
            adj3.AdjustQty = 2;
            adj3.AdjustType = "Deduct";
            adj3.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj3.TotalCost = 50;
            adj3.IssuedDate = DateTime.Now;
            adj3.VoucherNo = "0003";
            adj3.Comment = "None";
            adj3.Reason = "None";
            adj3.ItemID = 1;
            adj3.RequestByID = 1;
            adj3.ApprovedByID = 2;

            AdjustmentVoucher adj4 = new AdjustmentVoucher();
            adj4.AdjustQty = 10;
            adj4.AdjustType = "Deduct";
            adj4.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj4.TotalCost = 40;
            adj4.IssuedDate = DateTime.Now;
            adj4.VoucherNo = "0004";
            adj4.Comment = "None";
            adj4.Reason = "None";
            adj4.ItemID = 1;
            adj4.RequestByID = 1;
            adj4.ApprovedByID = 2;

            AdjustmentVoucher adj5 = new AdjustmentVoucher();
            adj5.AdjustQty = 1;
            adj5.AdjustType = "Deduct";
            adj5.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj5.TotalCost = 12;
            adj5.IssuedDate = DateTime.Now;
            adj5.VoucherNo = "0005";
            adj5.Comment = "None";
            adj5.Reason = "None";
            adj5.ItemID = 1;
            adj5.RequestByID = 1;
            adj5.ApprovedByID = 2;

            AdjustmentVoucher adj6 = new AdjustmentVoucher();
            adj6.AdjustQty = 30;
            adj6.AdjustType = "Deduct";
            adj6.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj6.TotalCost = 40;
            adj6.IssuedDate = DateTime.Now;
            adj6.VoucherNo = "0006";
            adj6.Comment = "None";
            adj6.Reason = "None";
            adj6.ItemID = 14;
            adj6.RequestByID = 1;
            adj6.ApprovedByID = 2;

            AdjustmentVoucher adj7 = new AdjustmentVoucher();
            adj7.AdjustQty = 30;
            adj7.AdjustType = "Deduct";
            adj7.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj7.TotalCost = 100;
            adj7.IssuedDate = DateTime.Now;
            adj7.VoucherNo = "0007";
            adj7.Comment = "None";
            adj7.Reason = "None";
            adj7.ItemID = 16;
            adj7.RequestByID = 1;
            adj7.ApprovedByID = 2;

            AdjustmentVoucher adj8 = new AdjustmentVoucher();
            adj8.AdjustQty = 48;
            adj8.AdjustType = "Deduct";
            adj8.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj8.TotalCost = 60;
            adj8.IssuedDate = DateTime.Now;
            adj8.VoucherNo = "0008";
            adj8.Comment = "None";
            adj8.Reason = "None";
            adj8.ItemID = 21;
            adj8.RequestByID = 1;
            adj8.ApprovedByID = 2;

            AdjustmentVoucher adj9 = new AdjustmentVoucher();
            adj9.AdjustQty = 20;
            adj9.AdjustType = "Deduct";
            adj9.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj9.TotalCost = 400;
            adj9.IssuedDate = DateTime.Now;
            adj9.VoucherNo = "0009";
            adj9.Comment = "None";
            adj9.Reason = "None";
            adj9.ItemID = 19;
            adj9.RequestByID = 1;
            adj9.ApprovedByID = 2;

            AdjustmentVoucher adj10 = new AdjustmentVoucher();
            adj10.AdjustQty = 40;
            adj10.AdjustType = "Deduct";
            adj10.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj10.TotalCost = 30;
            adj10.IssuedDate = DateTime.Now;
            adj10.VoucherNo = "0010";
            adj10.Comment = "None";
            adj10.Reason = "None";
            adj10.ItemID = 40;
            adj10.RequestByID = 1;
            adj10.ApprovedByID = 2;

            AdjustmentVoucher adj11 = new AdjustmentVoucher();
            adj11.AdjustQty = 40;
            adj11.AdjustType = "Deduct";
            adj11.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj11.TotalCost = 500;
            adj11.IssuedDate = DateTime.Now;
            adj11.VoucherNo = "0011";
            adj11.Comment = "None";
            adj11.Reason = "None";
            adj11.ItemID = 16;
            adj11.RequestByID = 1;
            adj11.ApprovedByID = 2;

            AdjustmentVoucher adj12 = new AdjustmentVoucher();
            adj12.AdjustQty = 9;
            adj12.AdjustType = "Deduct";
            adj12.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj12.TotalCost = 640;
            adj12.IssuedDate = DateTime.Now;
            adj12.VoucherNo = "0012";
            adj12.Comment = "None";
            adj12.Reason = "None";
            adj12.ItemID = 11;
            adj12.RequestByID = 1;
            adj12.ApprovedByID = 2;

            AdjustmentVoucher adj13 = new AdjustmentVoucher();
            adj13.AdjustQty = 10;
            adj13.AdjustType = "Deduct";
            adj13.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj13.TotalCost = 200;
            adj13.IssuedDate = DateTime.Now;
            adj13.VoucherNo = "0013";
            adj13.Comment = "None";
            adj13.Reason = "None";
            adj13.ItemID = 3;
            adj13.RequestByID = 1;
            adj13.ApprovedByID = 2;

            AdjustmentVoucher adj14 = new AdjustmentVoucher();
            adj14.AdjustQty = 10;
            adj14.AdjustType = "Deduct";
            adj14.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj14.TotalCost = 400;
            adj14.IssuedDate = DateTime.Now;
            adj14.VoucherNo = "0014";
            adj14.Comment = "None";
            adj14.Reason = "None";
            adj14.ItemID = 1;
            adj14.RequestByID = 1;
            adj14.ApprovedByID = 2;

            AdjustmentVoucher adj15 = new AdjustmentVoucher();
            adj15.AdjustQty = 14;
            adj15.AdjustType = "Deduct";
            adj15.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj15.TotalCost = 400;
            adj15.IssuedDate = DateTime.Now;
            adj15.VoucherNo = "0015";
            adj15.Comment = "None";
            adj15.Reason = "None";
            adj15.ItemID = 1;
            adj15.RequestByID = 1;
            adj15.ApprovedByID = 2;

            AdjustmentVoucher adj16 = new AdjustmentVoucher();
            adj16.AdjustQty = 10;
            adj16.AdjustType = "Deduct";
            adj16.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj16.TotalCost = 400;
            adj16.IssuedDate = DateTime.Now;
            adj16.VoucherNo = "0016";
            adj16.Comment = "None";
            adj16.Reason = "None";
            adj16.ItemID = 1;
            adj16.RequestByID = 1;
            adj16.ApprovedByID = 2;

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
