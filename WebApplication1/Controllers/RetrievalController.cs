using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static LUSS_API.Models.Status;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RetrievalController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<RetrievalController> _logger;
        public RetrievalController(ILogger<RetrievalController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<Retrieval> GetRetrievals()
        {
            List<Retrieval> retrievals = context123.Retrieval.ToList();
            return retrievals;
        }

        [HttpGet("{status}")]
        public IEnumerable<dynamic> GetRequestByStatus(string status)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
            List<Request> requests = context123.Request.ToList();
            List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
            List<Item> items = context123.Item.ToList();
            List<ItemPrice> prices = context123.ItemPrice.ToList();

            var iter = (from r in requests
                        join rd in requestDetailsList on r.RequestID equals rd.RequestID
                        where r.RequestStatus.Equals(st)
                        group rd by rd.ItemID into n
                        join i in items on n.FirstOrDefault().ItemID equals i.ItemID
                        select new
                        {
                            ItemID = i.ItemID,
                            ItemCode = i.ItemCode,
                            ItemName = i.ItemName,
                            UOM = i.UOM,
                            ItemPrice = prices.Where(x => x.ItemID == i.ItemID).FirstOrDefault().Price,
                            Location = i.StoreItemLocation,
                            InStock = i.InStockQty,
                            Category = i.ItemCategory.CategoryName,
                            TotalQty = n.Sum(x => x.RequestQty)
                        }).ToList();
            return iter;
        }

        // GET api/<controller>/5
        //[HttpGet("{status}")]
        //public string[] GetRetrivalForm(Status.EOrderStatus status)
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
