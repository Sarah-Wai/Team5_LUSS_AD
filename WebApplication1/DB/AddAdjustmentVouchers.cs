using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.DB
{
    public class AddAdjustmentVouchers
    {
        public static List<AdjustmentVoucher> getAllAdjustmentVoucher(List<User> users, List<Item> items)
        {
            List<AdjustmentVoucher> adjustmentVouchers = new List<AdjustmentVoucher>();

            AdjustmentVoucher adj1 = new AdjustmentVoucher();
            adj1.AdjustQty = 5;
            adj1.AdjustType = "Add";
            adj1.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj1.TotalCost = 25;
            adj1.IssuedDate = DateTime.Now.AddDays(-10);
            adj1.VoucherNo = DateTime.Now.AddDays(-10).ToString("yyMMddHHmmss");
            adj1.Comment = "None";
            adj1.Reason = "None";
            adj1.ItemID = items[0].ItemID;
            adj1.RequestByID = users[8].UserID;

            AdjustmentVoucher adj2 = new AdjustmentVoucher();
            adj2.AdjustQty = 4;
            adj2.AdjustType = "Deduct";
            adj2.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj2.TotalCost = 270;
            adj2.IssuedDate = DateTime.Now.AddDays(-9);
            adj2.VoucherNo = DateTime.Now.AddDays(-9).ToString("yyMMddHHmmss");
            adj2.Comment = "None";
            adj2.Reason = "None";
            adj2.ItemID = items[2].ItemID;
            adj2.RequestByID = users[8].UserID;


            AdjustmentVoucher adj3 = new AdjustmentVoucher();
            adj3.AdjustQty = 2;
            adj3.AdjustType = "Deduct";
            adj3.Status = AdjustmentVoucherStatus.AdjustmentStatus.Approved;
            adj3.TotalCost = 50;
            adj3.IssuedDate = DateTime.Now.AddDays(-8);
            adj3.VoucherNo = DateTime.Now.AddDays(-8).ToString("yyMMddHHmmss");
            adj3.Comment = "None";
            adj3.Reason = "None";
            adj3.ItemID = items[5].ItemID;
            adj3.RequestByID = users[8].UserID;
            adj3.ApprovedByID = users[7].UserID;

            AdjustmentVoucher adj4 = new AdjustmentVoucher();
            adj4.AdjustQty = 10;
            adj4.AdjustType = "Deduct";
            adj4.Status = AdjustmentVoucherStatus.AdjustmentStatus.Approved;
            adj4.TotalCost = 40;
            adj4.IssuedDate = DateTime.Now.AddHours(-2);
            adj4.VoucherNo = DateTime.Now.AddDays(-2).ToString("yyMMddHHmmss");
            adj4.Comment = "None";
            adj4.Reason = "None";
            adj4.ItemID = items[14].ItemID;
            adj4.RequestByID = users[8].UserID;
            adj4.ApprovedByID = users[6].UserID;

            AdjustmentVoucher adj5 = new AdjustmentVoucher();
            adj5.AdjustQty = 1;
            adj5.AdjustType = "Deduct";
            adj5.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj5.TotalCost = 12;
            adj5.IssuedDate = DateTime.Now.AddHours(-20);
            adj5.VoucherNo = DateTime.Now.AddHours(-20).ToString("yyMMddHHmmss");
            adj5.Comment = "None";
            adj5.Reason = "None";
            adj5.ItemID = items[36].ItemID;
            adj5.RequestByID = users[8].UserID;


            AdjustmentVoucher adj6 = new AdjustmentVoucher();
            adj6.AdjustQty = 30;
            adj6.AdjustType = "Deduct";
            adj6.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj6.TotalCost = 40;
            adj6.IssuedDate = DateTime.Now.AddDays(-20);
            adj6.VoucherNo = DateTime.Now.AddDays(-20).ToString("yyMMddHHmmss");
            adj6.Comment = "None";
            adj6.Reason = "None";
            adj6.ItemID = items[14].ItemID;
            adj6.RequestByID = users[8].UserID;


            AdjustmentVoucher adj7 = new AdjustmentVoucher();
            adj7.AdjustQty = 30;
            adj7.AdjustType = "Deduct";
            adj7.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj7.TotalCost = 100;
            adj7.IssuedDate = DateTime.Now;
            adj7.VoucherNo = DateTime.Now.ToString("yyMMddHHmmss");
            adj7.Comment = "None";   
            adj7.Reason = "None";
            adj7.ItemID = items[55].ItemID;
            adj7.RequestByID = users[8].UserID;


            AdjustmentVoucher adj8 = new AdjustmentVoucher();
            adj8.AdjustQty = 48;
            adj8.AdjustType = "Add";
            adj8.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj8.TotalCost = 60;
            adj8.IssuedDate = DateTime.Now.AddDays(-10);
            adj8.VoucherNo = DateTime.Now.AddDays(-10).ToString("yyMMddHHmmss");
            adj8.Comment = "None";
            adj8.Reason = "None";
            adj8.ItemID = items[21].ItemID;
            adj8.RequestByID = users[8].UserID;

            AdjustmentVoucher adj9 = new AdjustmentVoucher();
            adj9.AdjustQty = 20;
            adj9.AdjustType = "Add";
            adj9.Status = AdjustmentVoucherStatus.AdjustmentStatus.Rejected;
            adj9.TotalCost = 400;
            adj9.IssuedDate = DateTime.Now.AddDays(-8);
            adj9.VoucherNo = DateTime.Now.AddDays(-8).ToString("yyMMddHHmmss");
            adj9.Comment = "None";
            adj9.Reason = "misplaced in another box";
            adj9.ItemID = items[19].ItemID;
            adj9.RequestByID = users[8].UserID;
            adj9.ApprovedByID = users[6].UserID;

            AdjustmentVoucher adj10 = new AdjustmentVoucher();
            adj10.AdjustQty = 40;
            adj10.AdjustType = "Deduct";
            adj10.Status = AdjustmentVoucherStatus.AdjustmentStatus.Approved;
            adj10.TotalCost = 30;
            adj10.IssuedDate = DateTime.Now.AddDays(-5);
            adj10.VoucherNo = DateTime.Now.AddDays(-5).ToString("yyMMddHHmmss");
            adj10.Comment = "None";
            adj10.Reason = "None";
            adj10.ItemID = items[40].ItemID;
            adj10.RequestByID = users[8].UserID;
            adj10.ApprovedByID = users[7].UserID;

            AdjustmentVoucher adj11 = new AdjustmentVoucher();
            adj11.AdjustQty = 40;
            adj11.AdjustType = "Deduct";
            adj11.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj11.TotalCost = 500;
            adj11.IssuedDate = DateTime.Now.AddDays(-3);
            adj11.VoucherNo = DateTime.Now.AddDays(-3).ToString("yyMMddHHmmss");
            adj11.Comment = "None";
            adj11.Reason = "None";
            adj11.ItemID = items[60].ItemID;
            adj11.RequestByID = users[8].UserID;


            AdjustmentVoucher adj12 = new AdjustmentVoucher();
            adj12.AdjustQty = 9;
            adj12.AdjustType = "Deduct";
            adj12.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adj12.TotalCost = 640;
            adj12.IssuedDate = DateTime.Now.AddDays(-4);
            adj12.VoucherNo = DateTime.Now.AddDays(-4).ToString("yyMMddHHmmss");
            adj12.Comment = "None";
            adj12.Reason = "None";
            adj12.ItemID = items[56].ItemID;
            adj12.RequestByID = users[8].UserID;

            AdjustmentVoucher adj13 = new AdjustmentVoucher();
            adj13.AdjustQty = 10;
            adj13.AdjustType = "Deduct";
            adj13.Status = AdjustmentVoucherStatus.AdjustmentStatus.Rejected;
            adj13.TotalCost = 200;
            adj13.IssuedDate = DateTime.Now.AddDays(-1);
            adj13.VoucherNo = "VN" + DateTime.Now.AddDays(-1).ToString("yyMMddHHmmss");
            adj13.Comment = "Reuse";
            adj13.Reason = "None";
            adj13.ItemID = items[3].ItemID;
            adj13.RequestByID = users[8].UserID;
            adj13.ApprovedByID = users[7].UserID;

            AdjustmentVoucher adj14 = new AdjustmentVoucher();
            adj14.AdjustQty = 10;
            adj14.AdjustType = "Deduct";
            adj14.Status = AdjustmentVoucherStatus.AdjustmentStatus.Approved;
            adj14.TotalCost = 400;
            adj14.IssuedDate = DateTime.Now.AddDays(-1);
            adj14.VoucherNo = "VN" + DateTime.Now.AddDays(-1).ToString("yyMMddHHmmss");
            adj14.Comment = "None";
            adj14.Reason = "Broken";
            adj14.ItemID = items[66].ItemID;
            adj14.RequestByID = users[8].UserID;
            adj14.ApprovedByID = users[6].UserID;


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

            return adjustmentVouchers;
        }
    }
}
