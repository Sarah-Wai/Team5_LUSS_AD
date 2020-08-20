﻿using System;
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

        //public ViewResult GetAdjustmentVoucherByRequestor()
        //{
        //    return View("AdjustVoucherListingClerk");
        //}

        //public ViewResult AddAdjustmentVoucher() => View();

        //[HttpPost]
        //public async Task<IActionResult> AddAdjustmentVoucher(AdjustmentVoucher adj)
        //{
        //    AdjustmentVoucher adjustment = new AdjustmentVoucher();
        //    using (var httpClient = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(adj), Encoding.UTF8, "application/json");

        //        using (var response = await httpClient.PostAsync(api_url, content))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            adjustment = JsonConvert.DeserializeObject<AdjustmentVoucher>(apiResponse);
        //        }
        //    }

        //    ViewData["adjustment"] = adjustment;
        //    //return View(adjustment);
        //    return RedirectToAction("RetrievalForm", "Disbursement");
        //}

        [HttpPost]
        public async Task<IActionResult> AddAdjustmentVoucher(string adjustType, int itemId, int adjustQty, string reason, int userId, string entryPoint)
        {
            //AdjustmentVoucher adjustment = new AdjustmentVoucher();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/addAdjustment/" + adjustType + "/" + itemId + "/" + adjustQty + "/" + reason + "/" + userId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //adjustment = JsonConvert.DeserializeObject<AdjustmentVoucher>(apiResponse);
                }
            }
            //return View(adjustment);
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

                using (var response = await httpClient.GetAsync(api_url_itemPrice + "getPrice/" +adjustment.ItemID))
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
