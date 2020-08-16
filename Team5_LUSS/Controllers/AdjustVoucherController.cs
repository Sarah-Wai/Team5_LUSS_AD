using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAdjustmentVoucherByRequestor(int id)
        {
            id = 1;
            List<AdjustmentVoucher> adjustments = new List<AdjustmentVoucher>();
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/requestorId/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    adjustments = JsonConvert.DeserializeObject<List<AdjustmentVoucher>>(apiResponse);
                   

                }
            }
            ViewData["adjustmentByRequestor"] = adjustments;
            return View("AdjustVoucherListingClerk");
        }

        //public ViewResult GetAdjustmentVoucherByRequestor()
        //{
        //    return View("AdjustVoucherListingClerk");
        //}

        [HttpPost]
        public async Task<IActionResult> AddAdjustmentVoucher(AdjustmentVoucher adj)
        {
            AdjustmentVoucher adjustment = new AdjustmentVoucher();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(adj), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(api_url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    adjustment = JsonConvert.DeserializeObject<AdjustmentVoucher>(apiResponse);
                }
            }

            ViewData["adjustment"] = adjustment;
            return View(adjustment);
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
   
        //[HttpPost]
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

                using (var response = await httpClient.GetAsync(api_url_itemPrice + "/" +adjustment.ItemID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    price = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }
            ViewData["price"] = price;
            ViewData["adjustmentById"] = adjustment;
            return View("AdjustVoucherDetailsClerk");
        }

        //public ViewResult GetAdjustmentVoucherById()
        //{
        //    return View("AdjustVoucherDetailsClerk");
        //}


        public IActionResult Index()
        {
            return View("AdjustVoucherListingClerk");
        }
    }
}
