﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using static LUSS_API.Models.PurchaseOrderStatus;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseOrderController : Controller
    {
        public MyDbContext context123;
        private readonly ILogger<CollectionPointController> _logger;
        public PurchaseOrderController(ILogger<CollectionPointController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<PurchaseOrder> GetAllPurchaseOrders()
        {
            List<PurchaseOrder> purchaseList = context123.PurchaseOrder.ToList();
            return purchaseList;
        }


        [HttpGet("{id}")]
        public PurchaseOrder GetPurchaseOrderById(int id)
        {
            PurchaseOrder purchase = context123.PurchaseOrder.First(x => x.POID == id);
            return purchase;
        }

        [HttpGet("get/new-po-id")]
        public int GetNewPOId()
        {
            int maxId = 0;
            int? currentId = context123.PurchaseOrder.Max(x => x.POID);
            if (currentId != null)
            {
                maxId = (int)currentId;
            }
            return maxId + 1;
        }


        [HttpPost]
        public async Task<ActionResult<PurchaseOrder>> Post(PurchaseOrder po, int itemID, int orderQty, int supplierId)
        {
            //, int itemID, int orderQty, int supplierId
            po.POID = GetNewPOId();
            po.PONo = "PO " + po.POID;
            po.SupplierID = supplierId;
            po.Status = POStatus.Pending;
            po.Supplier = null;
            po.CreatedOn = DateTime.Now;
            context123.PurchaseOrder.Add(po);
            try {
                context123.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

            return CreatedAtAction(nameof(GetPurchaseOrderById), po);
        }


        [HttpGet("{po}/{itemID}/{orderQty}/{supllierId}")]
        public async Task<ActionResult<string>> savePO(PurchaseOrder po, int itemID, int orderQty, int supplierId)
        {
            po.POID = GetNewPOId();
            po.PONo = "PO " + po.POID;
            //po.SupplierID = supplierId;
            po.Status = POStatus.Pending;
            po.CreatedOn = DateTime.Now;
            context123.PurchaseOrder.Add(po);
            await context123.SaveChangesAsync();
            return "Ok";
        }
    }
}
