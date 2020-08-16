using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;

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

        public async Task<IActionResult> POCreateLow(int id)
        {
            Item item = new Item();
            List<Supplier> suppliers = new List<Supplier>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_Item + "/GetItemById/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Item>(apiResponse);
                }
                using (var response = await httpClient.GetAsync(api_url_ItemPrice + "/get-supplier-by-item/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    suppliers = JsonConvert.DeserializeObject<List<Supplier>>(apiResponse);
                }
            }

            ViewData["item"] = item;
            ViewData["suppliers"] = suppliers;
            return View("PO_Create_Low");
        }

        [HttpPost]
        public async Task<IActionResult> POCreateLow(int itemID, int orderQty, DateTime expectedDate, int supplierID)
        {
       
            return RedirectToAction("ViewLowStockItems");
        }

        public IActionResult Index()
        {
            //return View();
            //return View("PO_LowStock");
            //return View("PO_History");
            //return View("PO_Receive");
            //return View("PO_Create");
            //return View("PO_Create_Bulk");
            return View("PO_Create_Low");
        }
    }
}