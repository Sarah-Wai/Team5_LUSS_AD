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
            List<AdjustmentVoucher> adjustmentVouchers = context123.AdjustmentVouncher.ToList();
            return adjustmentVouchers;

        }

        [HttpGet("{id}")]
        public AdjustmentVoucher GetAdjustmentVoucherByID(int id)
        {
            AdjustmentVoucher iDVoucher = context123.AdjustmentVouncher.First(c => c.AdjustmentID == id);

            return iDVoucher;
        }

        [HttpGet("{AdjustmentID}/{ItemID}/{AdjustQty}/{AdjustType}")]
        [Route("ApprovedAdjustmentVoucher/{AdjustmentID}/{status}")]
        public string SaveVoucher(int AdjustmentID, AdjustmentStatus status)
        {
            AdjustmentVoucher adjustmentVouncher = context123.AdjustmentVouncher.First(c => c.AdjustmentID == AdjustmentID);

            if (adjustmentVouncher != null)
            {
                // do the changes to db
                adjustmentVouncher.Status = status;
                
            }
            Item item = context123.Item.First(c => c.ItemID == adjustmentVouncher.ItemID);

            if (item != null)
            {
                item.InStockQty =+ adjustmentVouncher.AdjustQty;
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

        [HttpGet("get-manager/{id}")]
        public User GetReportToByID(int id)
        {
            User requester = context123.User.First(x => x.UserID == id);
            User manager = context123.User.First(x => x.UserID == requester.ReportToID);   
            return manager;
        }


    }
}
