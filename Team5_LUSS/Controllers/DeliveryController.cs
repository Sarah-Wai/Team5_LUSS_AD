using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Team5_LUSS.Models;
using static Team5_LUSS.Models.Status;

namespace Team5_LUSS.Controllers
{
    public class DeliveryController : Controller
    {
        string api_url_rqst = "https://localhost:44312/Request";
        public async Task<IActionResult> ConfirmDelivery(string status)
        {
            List<Request> allRequests = new List<Request>();

            if(status == null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url_rqst + "/getAllRequest"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        allRequests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                    }

                }
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url_rqst + "/" + "GetRequestByStatus" + "/" + status))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        allRequests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                    }

                }
            }


            //List<Request> pendingRequests = allRequests.Where(x => x.RequestStatus == st).ToList();
            //List<Request> receivedRequests = allRequests.Where(x => x.RequestStatus == Status.EOrderStatus.Received).ToList();
            //List<Request> completedRequests = allRequests.Where(x => x.RequestStatus == Status.EOrderStatus.Completed).ToList();

            ViewData["allRqt"] = allRequests;

            /*ViewData["pendingRqt"] = pendingRequests;
            ViewData["receivedRqt"] = receivedRequests;
            ViewData["completedRqt"] = completedRequests;*/

            return View();
        }

    }
}
