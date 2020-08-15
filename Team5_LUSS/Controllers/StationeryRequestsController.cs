using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;
using System.Text;

namespace Team5_LUSS.Controllers
{
    public class StationeryRequestsController : Controller
    {
        string api_url = "https://localhost:44312/Request";
      
        public IActionResult StationeryRequests8()
        {
          
            return View();

        }

        public async Task<JsonResult> StationeryRequests(int id)
        {
            JsonResult result = null;

            List<Request> requests = new List<Request>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    requests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                }
            }
            //result = new JsonResult(requests);
            ViewData["requests"] = requests;
            return result;
        }

    }
}
