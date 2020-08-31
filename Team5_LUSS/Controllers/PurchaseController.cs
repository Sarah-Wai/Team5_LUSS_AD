using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;
using static Team5_LUSS.Models.PurchaseOrderStatus;

namespace Team5_LUSS.Controllers
{
    public class PurchaseController : Controller
    {
        string api_url = "https://localhost:44312/PurchaseOrder";
        string api_url_POdetails = "https://localhost:44312/PurchaseOrderItems";
        string api_url_Item = "https://localhost:44312/ItemList";
        string api_url_ItemPrice = "https://localhost:44312/ItemPrice";

        #region POHistory
        public async Task<IActionResult> PurchaseOrders(string status)
        {
            List<PurchaseOrder> purchases = new List<PurchaseOrder>();
            if (status == null)
            {             
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        purchases = JsonConvert.DeserializeObject<List<PurchaseOrder>>(apiResponse);
                    }
                }
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url + "/get-po-by-status/" + status))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        purchases = JsonConvert.DeserializeObject<List<PurchaseOrder>>(apiResponse);
                    }
                }

            }
            ViewData["purchases"] = purchases;
            return View("PO_History");
        }
        #endregion

        #region PODetails
        public async Task<IActionResult> PurchaseOrderDetails(int id)
        {
            PurchaseOrder purchase = new PurchaseOrder();
            List<PurchaseOrderItems> orderItems = new List<PurchaseOrderItems>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/get-po-by-id/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    purchase = JsonConvert.DeserializeObject<PurchaseOrder>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url_POdetails + "/POId/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    orderItems = JsonConvert.DeserializeObject<List<PurchaseOrderItems>>(apiResponse);
                }
            }

            ViewData["purchase"] = purchase;
            ViewData["orderItems"] = orderItems;
            return View("PO_Details");
        }
        #endregion

        #region LowStockItemList
        public async Task<IActionResult> ViewLowStockItems()
        {
            List<Item> lowStockItems = new List<Item>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_Item + "/get-low-stock-items"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    lowStockItems = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                }
            }
            ViewData["lowStockItems"] = lowStockItems;
            return View("PO_LowStock");
        }
        #endregion

        #region CreatePOForSingleLowStockItem
        public async Task<IActionResult> POCreateLow(int id)
        {
            Item item = new Item();
            List<Supplier> suppliers = new List<Supplier>();
            
            using (var httpClient = new HttpClient())
            {
                //get item by id
                using (var response = await httpClient.GetAsync(api_url_Item + "/GetItemById/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Item>(apiResponse);
                }
                //get supplier drop down list
                using (var response = await httpClient.GetAsync(api_url_ItemPrice + "/get-supplier-by-item/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    suppliers = JsonConvert.DeserializeObject<List<Supplier>>(apiResponse);
                }
            
            }

            ViewData["purchasedBy"] = (int)HttpContext.Session.GetInt32("UserID");
            ViewData["item"] = item;
            ViewData["suppliers"] = suppliers;
            return View("PO_Create_Low");
        }

        [HttpPost]
        public async Task<IActionResult> POCreateLow(string expectedDate, int itemID,  int supplierId, int orderQty)
        {

            //userId from session
            int userID = (int)HttpContext.Session.GetInt32("UserID");

            //string result;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + userID + "/" + expectedDate + "/" + itemID + "/"+ supplierId + "/" + orderQty))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //result = JsonConvert.DeserializeObject<String>(apiResponse);
                }
            }       
            return RedirectToAction("ViewLowStockItems");
        }
        #endregion

        #region CreatePOBulk
        [HttpPost]
        public async Task<IActionResult> POCreateBulk(List<int> itemId)
        {
            List<Item> items = new List<Item>();
            List<ItemPrice> itemsPrice = new List<ItemPrice>();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(itemId), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(api_url_ItemPrice + "/GetItemPriceByItemID/" + itemId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemsPrice = JsonConvert.DeserializeObject<List<ItemPrice>>(apiResponse);
                }
                using (var response = await httpClient.PostAsync(api_url_Item + "/get-items-by-id/" + itemId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                }
            }

            ViewData["poitems"] = items;
            ViewData["poItemPrice"] = itemsPrice;
            return View("PO_Create_Bulk");
        }

        [HttpPost]
        public async Task<IActionResult> POCreation(List<string> expectedDate, List<int> itemID, List<int> supplierId, List<int> orderQty)
        {
            //userId from session
            int userID = (int)HttpContext.Session.GetInt32("UserID");

            for (int i = 0; i < itemID.Count(); i++)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url + "/" + userID + "/" + expectedDate[i] + "/" + itemID[i] + "/" + supplierId[i] + "/" + orderQty[i]))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return RedirectToAction("ViewLowStockItems");
        }

        #endregion

        #region ReceivePO
        public async Task<IActionResult> ReceivePO(int id)
        {
            PurchaseOrder purchase = new PurchaseOrder();
            List<PurchaseOrderItems> orderItems = new List<PurchaseOrderItems>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/get-po-by-id/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    purchase = JsonConvert.DeserializeObject<PurchaseOrder>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url_POdetails + "/POId/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    orderItems = JsonConvert.DeserializeObject<List<PurchaseOrderItems>>(apiResponse);
                }
            }

            ViewData["purchase"] = purchase;
            ViewData["orderItems"] = orderItems;
            return View("PO_Receive");
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePO(List<int> receivedQty, int poid)
        {
            
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(receivedQty), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(api_url + "/received-purchase/" + receivedQty +"/" +poid, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();                  
                }
            }
            return RedirectToAction("PurchaseOrders");
        }
        #endregion

    }
}