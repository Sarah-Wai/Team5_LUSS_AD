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

        //for index home page
        [HttpGet]
        public IEnumerable<Item> GetAllItems()
        {
            List<Item> items = context123.Item.ToList();
            return items;
        }

        //for add function
        [HttpGet("{id}")]
        [Route("GetItemByID/{id}")]
        public Item GetItemByID(int id)
        {
            Item item = (from i in context123.Item
                         where i.ItemID == id
                         select i).FirstOrDefault();
            return item;
        }

        //for dropdownList
        [HttpGet("{id}")]
        [Route("GetItemListByCategoryID/{id}")]
        public IEnumerable<Item> GetItemListByCategoryID(int id)
        {
            IEnumerable<Item> itemList = context123.Item.Where(x => x.CategoryID.Equals(id)).ToList();
            return itemList;
        }

        //for search function
        [HttpGet("{id}/{name}")]
        [Route("FindByCatTDAndCatName/{id}/{name}")]
        public IEnumerable<Item> FindByCatTDAndCatName(int id, string name)
        {
            List<Item> itemList = new List<Item>();
            if (id == 0)
            {
                 itemList = context123.Item.Where(x => x.ItemName.Contains(name)).ToList();
            }
            itemList = context123.Item.Where(x => x.CategoryID.Equals(id) && x.ItemName.Contains(name)).ToList();
            return itemList;
        }

        //get low stock item list
        [HttpGet("get-low-stock-items")]
        public IEnumerable<Item> GetLowStockItems()
        {
            List<Item> items = context123.Item.Where(x => x.InStockQty < x.ReStockLevel).ToList();
            return items;
        }

        
    }
}