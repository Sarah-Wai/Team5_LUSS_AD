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
using Microsoft.AspNetCore.Http;

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
                int id = (int)HttpContext.Session.GetInt32("UserID");
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
                using (var response = await httpClient.GetAsync(api_url_rqst + "/" + "GetRequestCountByStatus" + "/" + "PendingDelivery"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pendingDeliveries = JsonConvert.DeserializeObject<int>(apiResponse);
                }
                using (var response = await httpClient.GetAsync(api_url_rqst + "/" + "GetRequestCountByStatus" + "/" + "Approved"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newRequests = JsonConvert.DeserializeObject<int>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url + "get-clerk-pending/"+ id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pendingAdjustments = Int32.Parse(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url + "get-next-collection-time-location/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    nextCollectionPoint = JsonConvert.DeserializeObject<CollectionPoint>(apiResponse);
                }
                using (var response = await httpClient.GetAsync(api_url + "get-next-collection-datetime/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    nextCollectionDate = JsonConvert.DeserializeObject<DateTime>(apiResponse);
                }
                using (var response = await httpClient.GetAsync(api_url + "get-low-stock-item-count/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lowStockItemCount = Int32.Parse(apiResponse);
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