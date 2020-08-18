using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Team5_LUSS.Models;

namespace Team5_LUSS.Controllers
{
    public class AdjustmentListController : Controller
    {
        string api_url = "https://localhost:44312/AdjustmentList"; // calling the right api
       
        public async Task<IActionResult> AdjustmentVouchers()
        {
            List<AdjustmentVoucher> adjustments = new List<AdjustmentVoucher>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url)) // call the api 
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    adjustments = JsonConvert.DeserializeObject<List<AdjustmentVoucher>>(apiResponse);
                }
            }
            ViewData["allAdjustments"] = adjustments;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Approved(int AdjustmentID)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + AdjustmentID + "/" + "Approve" + "/" + "ItemID" + "/" + "AdjustQty" +"/" + "AdjustType"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            string msg = "Assign Successfully!";
            return RedirectToAction("AssignRepresentative", new { id = 1, msg = msg });
        }

        [HttpGet]
        public async Task<IActionResult> Rejected(int AdjustmentID)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + AdjustmentID + "/" + "Reject" + "/" + "ItemID" + "/" + "AdjustQty" + "/" + "AdjustType"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            string msg = "Remove Successfully!";
            return RedirectToAction("AssignRepresentative", new { id = 1, msg = msg });
        }

    }
    /*
    [HttpPost]
        public async Task<IActionResult> VoucherApproveDetails(int AdjustmentID)
        {
            AdjustmentVoucher adjustmentVoucher = new AdjustmentVoucher();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url +"/"+ AdjustmentID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    adjustmentVoucher = JsonConvert.DeserializeObject<AdjustmentVoucher>(apiResponse);
                }
            }
            return View(adjustmentVoucher);
        }
    }*/
}
