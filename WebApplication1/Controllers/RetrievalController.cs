using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class RetrievalController : Controller
    {
        public MyDbContext context123;
        private readonly ILogger<CollectionPointController> _logger;
        public RetrievalController(ILogger<CollectionPointController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        // GET: api/<controller>
        [HttpGet("{status}")]
        public IEnumerable<Request> GetRetrievalForm(Status.EOrderStatus orderStatus)
        {
            orderStatus = Status.EOrderStatus.Approved;

            //var requests = from rh in context123.Request join rd in context123.RequestDetails on rh.RequestID equals rd.RequestID where rh.RequestStatus == orderStatus group rh by rd.ItemID into reqQtyPerItem;

            List<Request> requests = new List<Request>();
            return requests;

           
        }

        // GET api/<controller>/5
        [HttpGet("{status}")]
        public string[] GetRetrivalForm(Status.EOrderStatus status)
        {
            return new string[] { "value1", "value2" };
        }

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
