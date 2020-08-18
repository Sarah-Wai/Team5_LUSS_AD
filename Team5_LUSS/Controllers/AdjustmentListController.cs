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
        string api_url = "https://localhost:44312/AdjustmentVoucher/AdjustmentList";
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















    }
}
