using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LUSS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<RequestController> _logger;
        public RequestController(ILogger<RequestController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<Request> GetRequestList()
        {
           List<Request> requests = context123.Request.ToList();
            return requests;

        }

        [HttpGet("{id}")]
        public Request GetRequestByID(int id)
        {
            Request request = context123.Request.First(c => c.RequestID == id);

            return request;
        }

        [HttpPost]
        public async Task<ActionResult<ItemCategory>> SaveItemCategory(ItemCategory itemCategory)
        {
            context123.ItemCategory.Add(itemCategory);
            await context123.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRequestList), itemCategory);
        }


        [HttpPut]
        public ItemCategory Put([FromForm] ItemCategory res)
        {
            ItemCategory iCat = context123.ItemCategory
                  .Where(x => x.CategoryID == res.CategoryID).SingleOrDefault();
            iCat.CategoryName = res.CategoryName;
            context123.SaveChanges();
            return iCat;
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ItemCategory iCat = context123.ItemCategory.First(c => c.CategoryID == id);
            context123.ItemCategory.Remove(iCat);
            context123.SaveChanges();
        }
    }
}
   
