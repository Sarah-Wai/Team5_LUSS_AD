using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;

namespace Team5_LUSS.Controllers
{
    public class AdjustVoucherController : Controller
    {
        string api_url = "https://localhost:44312/AdjustmentVoucher";
        string api_url_itemPrice = "https://localhost:44312/ItemPrice";
        string api_url_itemList = "https://localhost:44312/ItemList";

        public async Task<IActionResult> AdjustmentVouchers()
        {
            List<AdjustmentVoucher> adjustments = new List<AdjustmentVoucher>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    adjustments = JsonConvert.DeserializeObject<List<AdjustmentVoucher>>(apiResponse);
                }
            }
            ViewData["allAdjustments"] = adjustments;
            return View();
        }

        //[HttpPost]
        public async Task<IActionResult> GetAdjustmentVoucherByRequestor(int id, string status)
        {
            id = (int)HttpContext.Session.GetInt32("UserID");
            List<AdjustmentVoucher> adjustments = new List<AdjustmentVoucher>();
            if(status == null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url + "/requestorId/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        adjustments = JsonConvert.DeserializeObject<List<AdjustmentVoucher>>(apiResponse);


                    }
                }
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url + "/GetRequestByIdByStatus/" + id + "/" + status))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        adjustments = JsonConvert.DeserializeObject<List<AdjustmentVoucher>>(apiResponse);
                    }
                }
            }
            
            ViewData["adjustmentByRequestor"] = adjustments;
            return View("AdjustVoucherListingClerk");
        }

        [HttpPost]
        public async Task<IActionResult> AddAdjustmentVoucher(string adjustType, int itemId, int adjustQty, string reason, int userId, string entryPoint)
        {
            int fromID = (int)HttpContext.Session.GetInt32("UserID");
            List<User> toID = new List<User>();
            

            using (var httpClient = new HttpClient())
            {
                if (reason == null)
                {
                    using (var response = await httpClient.GetAsync(api_url + "/addAdjustment/" + adjustType + "/" + itemId + "/" + adjustQty + "/" + userId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        toID = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                    }
                }
                else
                {
                    using (var response = await httpClient.GetAsync(api_url + "/addAdjustment/" + adjustType + "/" + itemId + "/" + adjustQty + "/" + reason + "/" + userId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        toID = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                    }
                }
            }

            foreach(User u in toID)
            {
                NotificationController.AdjustmentVoucherForApproval(fromID, u.UserID);
            }

            if (entryPoint.Equals("inventory"))
            {
                return RedirectToAction("InventoryList", "ItemList");
            }

            return RedirectToAction("RetrievalForm", "Disbursement");

        }

        public async Task<IActionResult> GetItemForAdjustment(int id)
        {
            Item item = new Item();
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_itemList + "/GetItemByID/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Item>(apiResponse);
                }
            }
            ViewData["item"] = item;
            return View("");
        }
   
        public async Task<IActionResult> GetAdjustmentVoucherById(int id)
        {
            AdjustmentVoucher adjustment = new AdjustmentVoucher();
            int price;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/adjustmentId/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    adjustment = JsonConvert.DeserializeObject<AdjustmentVoucher>(apiResponse);
                } 

                using (var response = await httpClient.GetAsync(api_url_itemPrice + "/getPrice/" + adjustment.ItemID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    price = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }
            ViewData["price"] = price;
            ViewData["adjustmentById"] = adjustment;
            return View("AdjustVoucherDetailsClerk");
        }

        public IActionResult Index()
        {
            return View("AdjustVoucherListingClerk");
        }
    }
}
