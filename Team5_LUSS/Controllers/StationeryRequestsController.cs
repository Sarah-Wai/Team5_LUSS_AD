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

       [HttpGet]
        public async Task<IActionResult> StationeryRequests()
        {
            int id = 1; //depID
            List<Request> requests = new List<Request>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url+ "/getAllRequestByDepID/"+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    requests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                }
            }
          
            ViewData["requests"] = requests;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ApproveRequestByDepHead(int id,int status, string comment)
        {
            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/ApproveRequestByDepHead/"+id+"/"+status + "/" + comment))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                   
                }
            }

            return RedirectToAction("StationeryRequests");
        }

    }
}
