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
        string api_url = "https://localhost:44312/api/Request";
      
        public async Task<IActionResult> StationeryRequests()
        {
            List<Request> requests = new List<Request>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    requests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                }
            }
          //  requests.Remove(RequestDetails);
            ViewData["requests"] = requests;
            return View();

        }

        public async Task<JsonResult> GetStationeryRequests(int id)
        {
            JsonResult result = null;

            Request itemCategory = new Request();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url +"/"+ id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemCategory = JsonConvert.DeserializeObject<Request>(apiResponse);
                    result = new JsonResult(itemCategory);
                   
                }
            }
           
            return result;
        }

    }
}
