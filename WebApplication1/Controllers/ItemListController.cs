using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using LUSS_API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static LUSS_API.Models.Status;

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
            List<Item> itemList = new List<Item>();
            if (id == 0)
            {
                itemList = context123.Item.ToList();
            }
            else
            {
                itemList = context123.Item.Where(x => x.CategoryID.Equals(id)).ToList();
            }

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
                if (!String.IsNullOrEmpty(name))
                {
                    itemList = context123.Item.Where(x => x.ItemName.Contains(name)).ToList();
                }
                else
                {
                    itemList = context123.Item.ToList();
                }

            }
            else
            {
                itemList = context123.Item.Where(x => x.CategoryID.Equals(id) && x.ItemName.Contains(name)).ToList();
            }

            return itemList;
        }

        //get low stock item list exclude those item with pending po
        [HttpGet]
        [Route("get-low-stock-items")]
        public IEnumerable<Item> GetLowStockItems()
        {
            List<Item> items = context123.Item.Where(x => x.InStockQty < x.ReStockLevel).ToList();

            List<int> poItemIds = context123.PurchaseOrderItems.Where(x => x.PurchaseOrder.Status == PurchaseOrderStatus.POStatus.Pending).Select(x => x.ItemID).Distinct().ToList();

            List<Item> lowStockList = new List<Item>();
            for(int i = 0; i < items.Count(); i++)
            {
                bool flag = false;
                //loop each item through list of pending poItemIds
                for (int j = 0; j < poItemIds.Count(); j++)
                {
                    //if item id in the list, break the inner loop 
                    if(items[i].ItemID == poItemIds[j])
                    {
                        flag = true;
                        break;
                    }
                    //if no flag and at final inner loop, add items into list
                    if (!flag && (j==poItemIds.Count-1))
                    {
                        lowStockList.Add(items[i]);
                    }
                }
            }
            return lowStockList;
        }

        //get selected Items
        [HttpPost("{itemId}")]
        [Route("get-items-by-id/{itemId}")]
        public IEnumerable<Item> GetItemListById(List<int> itemId)
        {
            List<Item> items = new List<Item>();
            for (int i = 0; i < itemId.Count(); i++)
            {
                items.Add(context123.Item.Where(x => x.ItemID == itemId[i]).FirstOrDefault());
            }
            return items;
        }
       
        //create new request
        [HttpPost]
        [Route("CreateRequest")]
        public Request CreateRequest([FromBody] string jsonData)
        {
            Request req = new Request();
            if (jsonData != null)
            {
                //try create new order first
                try
                {
                    req.RequestStatus = EOrderStatus.Pending;
                    req.RequestDate = DateTime.Now;
                    req.RequestBy = 1;
                    req.ModifiedBy = 1;
                    req.Comment = null;
                    req.RequestType = 0;
                    req.ParentRequestID = null;
                    req.CollectionTime = DateTime.Now;
                    req.RetrievalID = null;

                    context123.Request.Add(req);
                    context123.SaveChanges();
                    //Console.WriteLine(req.RequestID);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    try
                    {
                        List<AddToCartItem> items = JsonConvert.DeserializeObject<List<AddToCartItem>>(jsonData);
                        List<RequestDetails> reqDetails = new List<RequestDetails>();

                        foreach (var item in items)
                        {
                            RequestDetails reqDetail = new RequestDetails();
                            reqDetail.ItemID = item.ItemID;
                            reqDetail.RequestID = req.RequestID;
                            reqDetail.RequestQty = item.SelectedQty;
                            reqDetail.FullfillQty = null;
                            reqDetail.ReceivedQty = null;

                            reqDetails.Add(reqDetail);
                        }

                        context123.RequestDetails.AddRange(reqDetails);
                        context123.SaveChanges();
                    }
                    catch
                    {

                    }
                }
            } 
            return req;
        }

      

    }
}