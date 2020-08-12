using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemCategoryController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<ItemCategoryController> _logger;
        public ItemCategoryController(ILogger<ItemCategoryController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<ItemCategory> GetItemCategory()
        {
            List<ItemCategory> products = context123.ItemCategory.ToList();
            return products;

        }

        [HttpGet("{id}")]
        public ItemCategory GetItemCategoryByID(int id)
        {
            ItemCategory iCat = context123.ItemCategory.First(c => c.CategoryID == id);
         
            return iCat;
        }

        [HttpPost]
        public async Task<ActionResult<ItemCategory>> SaveItemCategory(ItemCategory itemCategory)
        {
            context123.ItemCategory.Add(itemCategory);
            await context123.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemCategory), itemCategory);
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
