using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemListController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<ItemListController> _logger;
        public ItemListController(ILogger<ItemListController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<Item> GetAllItems()
        {
            List<Item> items = context123.Item.ToList();
            return items;
        }

        
        [HttpGet("{id}")]
        [Route("GetItemByID/{id}")]
        public Item GetItemByID(int id)
        {
            Item item = (from i in context123.Item
                         where i.ItemID == id
                         select i).FirstOrDefault();
            return item;
        }

        [HttpGet("{id}")]
        [Route("GetItemListByCategoryID/{id}")]
        public IEnumerable<Item> GetItemListByCategoryID(int id)
        {
            IEnumerable<Item> itemList = context123.Item.Where(x => x.CategoryID.Equals(id)).ToList();                        
            return itemList;
        }

        [HttpPost]
        public async Task<ActionResult<Item>> SaveItem(Item item)
        {
            context123.Item.Add(item);
            await context123.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllItems), item);
        }

        [HttpPut]
        public Item Put([FromForm] Item item)
        {
            Item i = context123.Item
                  .Where(x => x.ItemID == item.ItemID).SingleOrDefault();
            i.ItemName = item.ItemName;
            i.UOM = item.UOM;
            i.ReStockQty = item.ReStockQty;
            i.InStockQty = item.InStockQty;
            i.CategoryID = item.CategoryID;
            i.ItemCode = item.ItemCode;
            i.ReStockLevel = item.ReStockLevel;
            i.StoreItemLocation = item.StoreItemLocation;
            context123.SaveChanges();
            return i;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Item item = (from i in context123.Item
                         where i.ItemID == id
                         select i).FirstOrDefault();
            context123.Item.Remove(item);
            context123.SaveChanges();
        }
    }
}