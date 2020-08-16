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
    public class RetrievalController : Controller
    {
        string api_url = "https://localhost:44312/Retrieval";

        public async Task<IActionResult> GetAllRetrieval()
        {
            List<Retrieval> retrievals = new List<Retrieval>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    retrievals = JsonConvert.DeserializeObject<List<Retrieval>>(apiResponse);
                }
            }

            ViewData["retrievals"] = retrievals;
            return View("Disbursement_By_Retrieval");
        }

        public async Task<IActionResult> RetrievalForm()
        {
            List<dynamic> items = new List<dynamic>();
            string status = "PendingDelivery";

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + status))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<dynamic>>(apiResponse);
                }
            }

            ViewData["items"] = items;
            return View("Retrieval_Form");
        }
    }
}