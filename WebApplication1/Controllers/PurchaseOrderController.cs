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

        [HttpGet("{status}")]
        [Route("get-po-by-status/{status}")]
        public IEnumerable<PurchaseOrder> GetAllPOByStatus(POStatus status)
        {
            List<PurchaseOrder> purchaseList = context123.PurchaseOrder.Where(x=>x.Status == status).ToList();
            return purchaseList;

        }

        [HttpGet("{id}")]
        [Route("get-po-by-id/{id}")]
        public PurchaseOrder GetPurchaseOrderById(int id)
        {
            PurchaseOrder purchase = context123.PurchaseOrder.First(x => x.POID == id);
            return purchase;
        }

        public int GetNewPOId()
        {
            int maxId = 0;
            List<PurchaseOrder> po = context123.PurchaseOrder.ToList();
            if (po.Count() > 0)
            {
                int? currentId = context123.PurchaseOrder.Max(x => x.POID);
                if (currentId != null)
                {
                    maxId = (int)currentId;
                }
            }
            return maxId + 1;
        }

        public int GetNewPOItemId()
        {
            int maxId = 0;
            List<PurchaseOrderItems> poItems = context123.PurchaseOrderItems.ToList();
            if (poItems.Count() > 0)
            {
                int? currentId = context123.PurchaseOrderItems.Max(x => x.POItemID);
                if (currentId != null)
                {
                    maxId = (int)currentId;
                }
            }
            return maxId + 1;
        }

        [HttpGet("{userid}/{expectedDate}/{itemID}/{supplierId}/{orderQty}")]
        public string savePO(int userid, string expectedDate, int itemID, int supplierId, int orderQty)
        {
            int poId = GetNewPOId();
            int poItemId = GetNewPOItemId();
            PurchaseOrder po = new PurchaseOrder()
            {
                PONo = "PO " + poId,
                CreatedOn = DateTime.Now,
                SupplierID = supplierId,
                Status = POStatus.Pending,
                ExpectedDate = Convert.ToDateTime(expectedDate),
                PurchasedBy = userid
            };
            PurchaseOrderItems poItem = new PurchaseOrderItems()
            {
                POID = poId,
                ItemID = itemID,
                OrderQty = orderQty,
            };
            context123.PurchaseOrder.Add(po);
            context123.PurchaseOrderItems.Add(poItem);
            context123.SaveChanges();
            return "Ok";
        }

        [HttpPost("{receivedQty}/{poid}")]
        [Route("received-purchase/{receivedQty}/{poid}")]
        public string receivedPurchase(List<int> receivedQty, int poid)
        {
            PurchaseOrder po = GetPurchaseOrderById(poid);
            List<PurchaseOrderItems> poItems = context123.PurchaseOrderItems.Where(x => x.POID == poid).ToList();
            //update recdQty for each poItems 
            for(int i = 0; i < poItems.Count() ; i++)
            {
                if (poItems[i].OrderQty >= receivedQty[i]) {
                    //update received qty
                    poItems[i].ReceivedQty = receivedQty[i];

                    //update instock qty
                    int itemId = poItems[i].ItemID;
                    Item item = context123.Item.Where(x => x.ItemID == itemId).FirstOrDefault();
                    item.InStockQty += receivedQty[i];
                }
            }
            //update PO
            po.Status = POStatus.Completed;
            po.ReceivedDate = DateTime.Now;
            context123.SaveChanges();
            return "ok";
        }

    }


}
