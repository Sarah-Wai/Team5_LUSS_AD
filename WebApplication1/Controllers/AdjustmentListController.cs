using LUSS_API.DB;
using LUSS_API.Models;
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
            List<AdjustmentVoucher> adjustmentpendingVoucherStatusDown = context123.AdjustmentVoucher.Where(x => x.Status == AdjustmentStatus.Pending && x.TotalCost <= 250).ToList();
            return adjustmentpendingVoucherStatusDown;

        }

        [HttpGet("{id}")]
        public AdjustmentVoucher GetAdjustmentVoucherByID(int id)
        {
            AdjustmentVoucher iDVoucher = context123.AdjustmentVoucher.First(c => c.AdjustmentID == id);

            return iDVoucher;
        }

        [HttpGet]
        [Route("ApprovedAdjustmentVoucher/{AdjustmentID}/{status}/{comment}")]
        public string SaveVoucher(int AdjustmentID, string status , string Comment)
        {
            AdjustmentVoucher adjustmentVouncher = context123.AdjustmentVoucher.First(c => c.AdjustmentID == AdjustmentID);
            AdjustmentStatus state = (AdjustmentStatus)Enum.Parse(typeof(AdjustmentStatus), status);
            if (status == "Approved")
            {
                // do the changes to db
                adjustmentVouncher.Status = state;
                adjustmentVouncher.Comment = Comment;


                Item item = context123.Item.First(c => c.ItemID == adjustmentVouncher.ItemID);

                if (item != null && adjustmentVouncher.AdjustType == "Deduct")
                {
                    item.InStockQty -= adjustmentVouncher.AdjustQty;
                }

                if (item != null && adjustmentVouncher.AdjustType == "Add")
                {
                    item.InStockQty += adjustmentVouncher.AdjustQty;
                }

            }

            if (status == "Rejected")
            {
                adjustmentVouncher.Status = state;
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


    }
}
