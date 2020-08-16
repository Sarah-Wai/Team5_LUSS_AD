using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class PurchaseOrderItemsController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<CollectionPointController> _logger;
        public PurchaseOrderItemsController(ILogger<CollectionPointController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<PurchaseOrderItems> GetAllPurchaseOrderItems()
        {
            List<PurchaseOrderItems> poItemsList = context123.PurchaseOrderItems.ToList();
            return poItemsList;
        }


        [HttpGet("POItemId/{id}")]
        public PurchaseOrderItems GetPurchaseOrderItemsById(int id)
        {
            PurchaseOrderItems poItem = context123.PurchaseOrderItems.First(x => x.POItemID == id);
            return poItem;
        }

        [HttpGet("POId/{id}")]
        public List<PurchaseOrderItems> GetAllPurchaseOrderItemsById(int id)
        {
            List<PurchaseOrderItems> orderItems = context123.PurchaseOrderItems
                .Where(x => x.POID == id).ToList();
            
            return orderItems;
        }


        [HttpPost]
        public async Task<ActionResult<PurchaseOrderItems>> Post([FromBody]PurchaseOrderItems poItem)
        {
            context123.PurchaseOrderItems.Add(poItem);
            await context123.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPurchaseOrderItemsById), poItem);
        }

    }
}