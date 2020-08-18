using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IActionResult> PurchaseOrders()
        {
            List<PurchaseOrder> purchases = new List<PurchaseOrder>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    purchases = JsonConvert.DeserializeObject<List<PurchaseOrder>>(apiResponse);
                }
            }

            ViewData["purchases"] = purchases;
            return View("PO_History");
        }

        public async Task<IActionResult> PurchaseOrderDetails(int id)
        {
            PurchaseOrder purchase = new PurchaseOrder();
            List<PurchaseOrderItems> orderItems = new List<PurchaseOrderItems>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + id))
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
            int poId;
            
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
                //get new POID
                using (var response = await httpClient.GetAsync(api_url + "/get/new-po-id"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    poId = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }

            string poNo = "PO" + poId;
            ViewData["purchasedBy"] = 1; // inject user session
            ViewData["item"] = item;
            ViewData["suppliers"] = suppliers;
            return View("PO_Create_Low");
        }

        [HttpPost]
        public async Task<IActionResult> POCreateLow( int id, string expectedDate, int itemID,  int supplierId, int orderQty)
        {
            //string result;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + id + "/" + expectedDate + "/" + itemID + "/"+ supplierId + "/" + orderQty))
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

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(itemId), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(api_url_Item + "/get-items-by-id/" + itemId, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                }
            }
            ViewData["poitems"] = items;
            return View("PO_Create_Bulk");
        }
            #endregion

        public IActionResult Index()
        {
            //return View();
            return View("PO_LowStock");
            //return View("PO_History");
            //return View("PO_Receive");
            //return View("PO_Create");
            //return View("PO_Create_Bulk");
            //return View("PO_Create_Low");
        }
    }

    //public class Dummy
    //{
    //    List<int> ItemId;
    //}
}