using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpPost]
        public async Task<ActionResult<PurchaseOrder>> Post([FromBody]PurchaseOrder po)
        {
            context123.PurchaseOrder.Add(po);
            await context123.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPurchaseOrderById), po);
        }
    }
}
