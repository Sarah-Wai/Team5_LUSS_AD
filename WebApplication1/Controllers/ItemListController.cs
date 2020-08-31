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
            if (poItemIds.Count == 0) { lowStockList = items; }
            else
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    bool flag = false;
                    //loop each item through list of pending poItemIds
                    for (int j = 0; j < poItemIds.Count(); j++)
                    {
                        //if item id in the list, break the inner loop 
                        if (items[i].ItemID == poItemIds[j])
                        {
                            flag = true;
                            break;
                        }
                        //if no flag and at final inner loop, add items into list
                        if (!flag && (j == poItemIds.Count - 1))
                        {
                            lowStockList.Add(items[i]);
                        }
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
            ItemRequest itemreq = JsonConvert.DeserializeObject<ItemRequest>(jsonData);
            if (jsonData != null)
            {
                var datetime = DateTime.Now;
                //try create new order first
                try
                {
                    req.RequestStatus = EOrderStatus.Pending;
                    req.RequestDate = datetime;
                    req.RequestBy = itemreq.UserID;
                    req.ModifiedBy = itemreq.UserID;
                    req.Comment = null;
                    req.RequestType = 0;
                    req.ParentRequestID = null;
                    req.CollectionTime = datetime.AddYears(-10);
                    req.RetrievalID = null;
                    
                    context123.Request.Add(req);
                    context123.SaveChanges();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    try
                    {

                        List<AddToCartItem> items = itemreq.ItemList;
                        List<RequestDetails> reqDetails = new List<RequestDetails>();

                        foreach (var item in items)
                        {
                            RequestDetails reqDetail = new RequestDetails();
                            reqDetail.ItemID = item.ItemID;
                            reqDetail.RequestID = req.RequestID;
                            reqDetail.RequestQty = item.SelectedQty;
                            reqDetail.FullfillQty = null;
                            reqDetail.ReceivedQty = null;
                            reqDetail.isActive = true;

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

        [HttpGet("{userId}")]
        [Route("get-user-top/{userId}")]
        public List<TopSixRequested> GetUserTopItems(int userId)
        {

            var highestRequest = (from requests in context123.Request
                                  join requestDetails in context123.RequestDetails on requests.RequestID equals requestDetails.RequestID
                                  join item in context123.Item on requestDetails.ItemID equals item.ItemID
                                  where requests.RequestByUser.UserID == userId
                                  select new TopSixRequested
                                  {
                                      ItemID = requestDetails.ItemID,
                                      ItemName = item.ItemName,
                                      Qty = requestDetails.RequestQty,
                                  }).ToList();

            List<TopSixRequested> userTopItems = (highestRequest.GroupBy(x => x.ItemID).Select(y => new TopSixRequested
            {

                ItemID = y.First().ItemID,
                ItemName = y.First().ItemName,
                Qty = y.Sum(s => s.Qty),

            })).OrderByDescending(x => x.Qty).Take(6).ToList();

            return userTopItems;

        }

        [HttpGet]
        [Route("mobile")]
        public IEnumerable<CustomItem> GetItemList()
        {
            List<Item> items = context123.Item.ToList();
            List<CustomItem> itemList = new List<CustomItem>();

            foreach(Item i in items)
            {
                CustomItem c = new CustomItem
                {
                    ItemID = i.ItemID,
                    ItemCode = i.ItemCode,
                    ItemName = i.ItemName,
                    Location = i.StoreItemLocation,
                    UOM = i.UOM,
                    InStockQty = i.InStockQty,
                    ReOrderLevel = i.ReStockLevel,
                    ReOrderQty = i.ReStockQty,
                    CategoryName = i.ItemCategory.CategoryName
                };

                itemList.Add(c);
            }
            return itemList;
        }

        [HttpGet("{id}")]
        [Route("mobile/GetItemByID/{id}")]
        public CustomItem GetItemDetails(int id)
        {
            Item i = (from item in context123.Item
                         where item.ItemID == id
                         select item).FirstOrDefault();

            CustomItem custom = new CustomItem
            {
                ItemID = i.ItemID,
                ItemCode = i.ItemCode,
                ItemName = i.ItemName,
                Location = i.StoreItemLocation,
                UOM = i.UOM,
                InStockQty = i.InStockQty,
                ReOrderLevel = i.ReStockLevel,
                ReOrderQty = i.ReStockQty,
                CategoryName = i.ItemCategory.CategoryName
            };
            return custom;
        }



    }
}