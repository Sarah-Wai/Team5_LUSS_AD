﻿using System;
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
            List<AdjustmentVoucher> adjustments = context123.AdjustmentVouncher.ToList();
            return adjustments;
        }

        [HttpGet("requestorId/{id}")]
        public IEnumerable<AdjustmentVoucher> GetAdjustmentVoucherByRequestor(int id)
        {
            List<AdjustmentVoucher> adjustments = context123.AdjustmentVouncher
                .Where(x => x.RequestByID == id).ToList();
            return adjustments;
        }

        [HttpGet("adjustmentId/{id}")]
        public AdjustmentVoucher GetAdjustmentVoucherByID(int id)
        {
            AdjustmentVoucher adjustment = context123.AdjustmentVouncher.First(a => a.AdjustmentID == id);
            
            return adjustment;
        }

        
        [HttpPost]
        public async Task<ActionResult<AdjustmentVoucher>> SaveAdjustmentVoucher(AdjustmentVoucher adjustment)
        {
            int price = context123.ItemPrice
                .Where(x => x.ItemID == adjustment.ItemID).First().Price;

            adjustment.Status = AdjustmentVoucherStatus.AdjustmentStatus.Pending;
            adjustment.IssuedDate = DateTime.Now;
            adjustment.VoucherNo = "VN" + adjustment.AdjustmentID;
            adjustment.TotalCost = adjustment.AdjustQty * price;

            context123.AdjustmentVouncher.Add(adjustment);
            await context123.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdjustmentVoucher), adjustment);
        }

        //[HttpPost]
        //public async Task<ActionResult<AdjustmentVoucher>> SaveAdjustmentVoucher(string adjustment)
        //{
        //    var tempt = JsonConvert.DeserializeObject<dynamic>(adjustment);

        //    AdjustmentVoucher voucher = new AdjustmentVoucher();

        //    Console.WriteLine(tempt);

        //    return voucher;
        //}

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
