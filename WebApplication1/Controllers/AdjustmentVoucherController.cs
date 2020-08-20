using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LUSS_API.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class AdjustmentVoucherController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<AdjustmentVoucherController> _logger;
        public AdjustmentVoucherController(ILogger<AdjustmentVoucherController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<AdjustmentVoucher> GetAdjustmentVoucher()
        {
            List<AdjustmentVoucher> adjustments = context123.AdjustmentVoucher.ToList();
            return adjustments;
        }

        [HttpGet("requestorId/{id}")]
        public IEnumerable<AdjustmentVoucher> GetAdjustmentVoucherByRequestor(int id)
        {
            List<AdjustmentVoucher> adjustments = context123.AdjustmentVoucher
                .Where(x => x.RequestByID == id).ToList();
            return adjustments;
        }

        [HttpGet("{id}/{status}")]
        [Route("GetRequestByIdByStatus/{id}/{status}")]
        public IEnumerable<AdjustmentVoucher> GetRequestByStatus(int id,string status)
        {
            AdjustmentVoucherStatus.AdjustmentStatus st = (AdjustmentVoucherStatus.AdjustmentStatus)Enum.Parse(typeof(AdjustmentVoucherStatus.AdjustmentStatus), status);
            List<AdjustmentVoucher> adjustments = context123.AdjustmentVoucher.Where(x => x.Status == st).ToList();

            return adjustments;
        }

        [HttpGet("adjustmentId/{id}")]
        public AdjustmentVoucher GetAdjustmentVoucherByID(int id)
        {
            AdjustmentVoucher adjustment = context123.AdjustmentVoucher.First(a => a.AdjustmentID == id);
            
            return adjustment;
        }
     
        //[HttpPost]
        //public async Task<ActionResult<AdjustmentVoucher>> SaveAdjustmentVoucher(AdjustmentVoucher adjustment)
        //{
        //    int price = context123.ItemPrice
        //        .Where(x => x.ItemID == adjustment.ItemID).First().Price;

        //    adjustment.AdjustmentID = 1;
        //    adjustment.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
        //    adjustment.IssuedDate = DateTime.Now;
        //    adjustment.VoucherNo = "VN" + adjustment.AdjustmentID;
        //    adjustment.TotalCost = adjustment.AdjustQty * price;

        //    context123.AdjustmentVouncher.Add(adjustment);
        //    await context123.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetAdjustmentVoucher), adjustment);
        //}

        [HttpGet("{adjustType}/{itemId}/{adjustQty}/{reason}/{userId}")]
        [Route("addAdjustment/{adjustType}/{itemId}/{adjustQty}/{reason}/{userId}")]
        public string AddAdjustmentVoucher(string adjustType, int itemId, int adjustQty, string reason, int userId)
        {
            int price = context123.ItemPrice
                .Where(x => x.ItemID == itemId).FirstOrDefault().Price;
            //int id = GetNewAdjVoucherId();

            AdjustmentVoucher adjustment = new AdjustmentVoucher()
            {
                Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending,
                IssuedDate = DateTime.Now,
                RequestByID = userId,
                VoucherNo = "VN" + DateTime.Now.ToString("yyMMddHHmmss"),
                ItemID = itemId,
                Reason = reason,
                AdjustQty = adjustQty,
                AdjustType = adjustType,
                TotalCost = adjustQty * price
            };

            context123.AdjustmentVoucher.Add(adjustment);
            context123.SaveChanges();
            return "success";

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
