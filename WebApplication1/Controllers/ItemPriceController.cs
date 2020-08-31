using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using LUSS_API.Models.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemPriceController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<ItemPriceController> _logger;
        public ItemPriceController(ILogger<ItemPriceController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("getPrice/{id}")]
        public int GetItemPrice(int id)
        {
            ItemPrice itemPrice = context123.ItemPrice
                .Where(x => x.ItemID == id).FirstOrDefault();

            int price = itemPrice.Price;
            
            return price; 
        }


        [HttpGet("getItemDetails/{id}")]
        public ItemPrice GetItemById(int id)
        {
            ItemPrice item = context123.ItemPrice
                .Where(x => x.ItemID == id).FirstOrDefault();

            return item;
        }

        [HttpGet("getItem/{id}")]
        [Route("GetItemPriceByItemID/{id}")]
        public List<ItemPrice> GetItemById(List<int> id)
        {
            List<ItemPrice> itemPrice = new List<ItemPrice>();

            foreach (int i in id)
            {
                itemPrice.AddRange(context123.ItemPrice.Where(x => x.ItemID == i).ToList());
;           }
            return itemPrice;
        }

        //get supplier list by item
        [HttpGet("get-supplier-by-item/{id}")]
        public List<Supplier> GetSupplierByItem(int id) 
        {
            List<ItemPrice> itemPriceList = context123.ItemPrice.Where(x => x.ItemID == id).ToList();
            List<Supplier> suppliers = itemPriceList.Select(x => x.Supplier).ToList();
            return suppliers;
        }

        //Mobile API
        [HttpGet("mobile/getItemDetails/{id}")]
        public CustomAdjustmentVoucher GetItemDetails(int id)
        {
            ItemPrice item = context123.ItemPrice
                .Where(x => x.ItemID == id).FirstOrDefault();

            CustomAdjustmentVoucher voucher = new CustomAdjustmentVoucher
            {
                ItemID = item.ItemID,
                ItemCode = item.Item.ItemCode,
                ItemName = item.Item.ItemName,
                CategoryName = item.Item.ItemCategory.CategoryName,
                UOM = item.Item.UOM,
                ItemPrice = item.Price,
            };

            return voucher;
        }
    }
}
