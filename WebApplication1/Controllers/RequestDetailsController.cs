using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using LUSS_API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestDetailsController : Controller
    {
        public MyDbContext context123;
        private readonly ILogger<RequestDetailsController> _logger;
        public RequestDetailsController(ILogger<RequestDetailsController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet("get-by-request/{id}")]
        public List<RequestDetails> GetByRequest(int id)
        {
            List<RequestDetails> requestItems = context123.RequestDetails.Where(x => x.RequestID == id).ToList();
            return requestItems;
        }

        // for mobile
        [HttpGet("{id}")]
        [Route("get-by-request-mobile/{id}")]
        public List<CustomRequestDetail> MGetByRequest(int id)
        {
            List<RequestDetails> requestItems = context123.RequestDetails.Where(x => x.RequestID == id).ToList();
            List<CustomRequestDetail> customReqItems = new List<CustomRequestDetail>();
            foreach(RequestDetails r in requestItems)
            {
                CustomRequestDetail c = new CustomRequestDetail()
                {
                    RequestDetailID = r.RequestDetailID,
                    RequestQty = r.RequestQty,
                    ItemID = r.ItemID,
                    RequestID = r.RequestID,
                    FulfillQty = r.FullfillQty,
                    ItemCode = r.Item.ItemCode,
                    ItemName = r.Item.ItemName,
                    UOM = r.Item.UOM,
                    inStockQty = r.Item.InStockQty,
                    ReceivedQty = r.ReceivedQty
                };
                customReqItems.Add(c);
            }
            
            return customReqItems;
        }

    }
}
