using LUSS_API.DB;
using LUSS_API.Models;
using LUSS_API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static LUSS_API.Models.AdjustmentVoucherStatus;
using static LUSS_API.Models.Status;

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdjustmentListController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<AdjustmentListController> _logger;
        public AdjustmentListController(ILogger<AdjustmentListController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<AdjustmentVoucher> GetAdjustmentVoucher()
        {
            List<AdjustmentVoucher> adjustmentVouchers = context123.AdjustmentVoucher.ToList();
            return adjustmentVouchers;

        }

        [HttpGet("pendingUp")]
        public IEnumerable<AdjustmentVoucher> GetPendingAdjustmentVoucherStatusUp()
        {
            List<AdjustmentVoucher> adjustmentpendingVoucherStatusUp = context123.AdjustmentVoucher.Where(x => x.Status == AdjustmentStatus.Pending && x.TotalCost >= 250).ToList();
            return adjustmentpendingVoucherStatusUp;

        }

        [HttpGet("pendingDown")]
        public IEnumerable<AdjustmentVoucher> GetPendingAdjustmentVoucherStatusDown()
        {
            List<AdjustmentVoucher> adjustmentpendingVoucherStatusDown = context123.AdjustmentVoucher.Where(x => x.Status == AdjustmentStatus.Pending && x.TotalCost < 250).ToList();
            return adjustmentpendingVoucherStatusDown;

        }

        [HttpGet("{id}")]
        public AdjustmentVoucher GetAdjustmentVoucherByID(int id)
        {
            AdjustmentVoucher iDVoucher = context123.AdjustmentVoucher.First(c => c.AdjustmentID == id);

            return iDVoucher;
        }

        [HttpGet]
        [Route("ApprovedAdjustmentVoucher/{AdjustmentID}/{Comment}/{userID}/{status}")]
        public string SaveVoucher(int AdjustmentID, string Comment, int userID, string status)
        {
            AdjustmentVoucher adjustmentVouncher = context123.AdjustmentVoucher.First(c => c.AdjustmentID == AdjustmentID);
            AdjustmentStatus state = (AdjustmentStatus)Enum.Parse(typeof(AdjustmentStatus), status);
            if (status.Equals("Approved"))
            {
                // do the changes to db
                adjustmentVouncher.Status = state;
                adjustmentVouncher.Comment = Comment;
                adjustmentVouncher.ApprovedByID = userID;
                
            }
            Item item = context123.Item.First(c => c.ItemID == adjustmentVouncher.ItemID);

                if (item != null && adjustmentVouncher.AdjustType == "Deduct")
                {
                    item.InStockQty -= adjustmentVouncher.AdjustQty;
                }

                if (item != null && adjustmentVouncher.AdjustType == "Add")
                {
                    item.InStockQty += adjustmentVouncher.AdjustQty;
                }

            

            if (status.Equals("Rejected"))
            {
                adjustmentVouncher.Status = state;
                adjustmentVouncher.Comment = Comment;
                adjustmentVouncher.ApprovedByID = userID;
            }


                try
            {
                context123.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return "Success";
        }

        [HttpGet("get_manager/{id}")]
        public User GetReportToByID(int id)
        {
            User manager = new User();
            User requester = context123.User.First(x => x.UserID == id);
            if (requester.ReportToID != null) {
                 manager = context123.User.First(x => x.UserID == requester.ReportToID);
            }
             
            return manager;
        }

        //Mobile API
        [HttpGet("mobile/pendingUp")]
        public IEnumerable<CustomAdjustmentVoucher> GetPendingAdjustmentVoucherManager()
        {
            List<AdjustmentVoucher> adjustmentpendingVoucherStatusUp = context123.AdjustmentVoucher.Where(x => x.Status == AdjustmentStatus.Pending && x.TotalCost >= 250).ToList();
            List<CustomAdjustmentVoucher> adjustmentVouchers = new List<CustomAdjustmentVoucher>();
            
            foreach(AdjustmentVoucher a in adjustmentpendingVoucherStatusUp)
            {
                CustomAdjustmentVoucher c = new CustomAdjustmentVoucher
                {
                    AdjustmentID = a.AdjustmentID,
                    AdjustQty = a.AdjustQty,
                    AdjustType = a.AdjustType,
                    Status = a.Status,
                    TotalCost = a.TotalCost,
                    //IssuedDate = a.IssuedDate,
                    VoucherNo = a.VoucherNo,
                    Comment = a.Comment,
                    Reason = a.Reason,
                    ItemID = a.ItemID,
                    ApprovedByID = a.ApprovedByID,
                    RequestByID = a.RequestByID,
                    RequestedByUser = a.RequestedByUser.FirstName + " " + a.RequestedByUser.LastName,
                    ApprovedByUser = a.ApprovedByUser.FirstName + " " + a.ApprovedByUser.LastName,
                    ItemCode = a.Item.ItemCode,
                    ItemName = a.Item.ItemName,
                    CategoryName = a.Item.ItemCategory.CategoryName,
                    UOM = a.Item.UOM  
                };

                adjustmentVouchers.Add(c);
            }

            return adjustmentVouchers;

        }

        [HttpGet("mobile/pendingDown")]
        public IEnumerable<CustomAdjustmentVoucher> GetPendingAdjustmentVoucherSupervisor()
        {
            List<AdjustmentVoucher> adjustmentpendingVoucherStatusDown = context123.AdjustmentVoucher.Where(x => x.Status == AdjustmentStatus.Pending && x.TotalCost < 250).ToList();
            List<CustomAdjustmentVoucher> adjustmentVouchers = new List<CustomAdjustmentVoucher>();

            foreach (AdjustmentVoucher a in adjustmentpendingVoucherStatusDown)
            {
                CustomAdjustmentVoucher c = new CustomAdjustmentVoucher
                {
                    AdjustmentID = a.AdjustmentID,
                    AdjustQty = a.AdjustQty,
                    AdjustType = a.AdjustType,
                    Status = a.Status,
                    TotalCost = a.TotalCost,
                    //IssuedDate = a.IssuedDate,
                    VoucherNo = a.VoucherNo,
                    Comment = a.Comment,
                    Reason = a.Reason,
                    ItemID = a.ItemID,
                    ApprovedByID = a.ApprovedByID,
                    RequestByID = a.RequestByID,
                    RequestedByUser = a.RequestedByUser.FirstName + " " + a.RequestedByUser.LastName,
                    ItemCode = a.Item.ItemCode,
                    ItemName = a.Item.ItemName,
                    CategoryName = a.Item.ItemCategory.CategoryName,
                    UOM = a.Item.UOM
                };

                adjustmentVouchers.Add(c);
            }

            return adjustmentVouchers;

        }

        [HttpGet("mobile/{id}")]
        public CustomAdjustmentVoucher GetAdjustmentDetailsByID(int id)
        {
            AdjustmentVoucher iDVoucher = context123.AdjustmentVoucher.Where(c => c.AdjustmentID == id).FirstOrDefault();
            CustomAdjustmentVoucher voucher = new CustomAdjustmentVoucher
            {
                AdjustmentID = iDVoucher.AdjustmentID,
                AdjustQty = iDVoucher.AdjustQty,
                AdjustType = iDVoucher.AdjustType,
                Status = iDVoucher.Status,
                TotalCost = iDVoucher.TotalCost,
                IssuedDate = iDVoucher.IssuedDate,
                VoucherNo = iDVoucher.VoucherNo,
                Comment = iDVoucher.Comment,
                Reason = iDVoucher.Reason,
                ItemID = iDVoucher.ItemID,
                ApprovedByID = iDVoucher.ApprovedByID,
                RequestByID = iDVoucher.RequestByID,
                RequestedByUser = iDVoucher.RequestedByUser.FirstName + " " + iDVoucher.RequestedByUser.LastName,
                ItemCode = iDVoucher.Item.ItemCode,
                ItemName = iDVoucher.Item.ItemName,
                CategoryName = iDVoucher.Item.ItemCategory.CategoryName,
                UOM = iDVoucher.Item.UOM
            };
            return voucher;
        }


    }
}
