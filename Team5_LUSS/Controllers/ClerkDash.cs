using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Team5_LUSS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models.ViewModels;


namespace Team5_LUSS.Controllers
{
    public class ClerkDashController : Controller
    {

        string api_url = "https://localhost:44312/ClerkDash/";
        string api_url_rqst = "https://localhost:44312/Request";
        int newRequests = 0;
        int pendingDeliveries = 0;
        int pendingAdjustments = 0;
        int lowStockItemCount = 0;
        CollectionPoint nextCollectionPoint;
        DateTime nextCollectionDate;

        List<TopSixRequested> topSixRequested = new List<TopSixRequested>();

        public async Task<IActionResult> Index()
        {
            string name = "Clerk's Name";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    name = apiResponse;
                }

                using (var response = await httpClient.GetAsync(api_url+ "get-top-summed/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    topSixRequested = JsonConvert.DeserializeObject<List<TopSixRequested>>(apiResponse);
                }
                
                

            }
            ViewData["name"] = name;
            ViewData["topSixRequested"] = topSixRequested;
            ViewData["newRequests"] = newRequests;
            ViewData["pendingDeliveries"] = pendingDeliveries;
            ViewData["pendingAdjustments"] = pendingAdjustments;
            ViewData["nextCollectionPoint"] = nextCollectionPoint;
            ViewData["nextCollectionDate"] = nextCollectionDate;
            ViewData["lowStockItemCount"] = lowStockItemCount;





            return View("ClerkDash");
        }


    }

}