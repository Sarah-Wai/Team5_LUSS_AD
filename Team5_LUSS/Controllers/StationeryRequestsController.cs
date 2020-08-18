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
        string api_url = "https://localhost:44312/";

        [HttpGet]
        public async Task<IActionResult> StationeryRequests()
        {
            
            List<Request> requests = new List<Request>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url+ "Request/GetAllRequest"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    requests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                }
            }
          
            ViewData["requests"] = requests;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRequestByDepHead(int hidRequestID, string comment)
        {
            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "Request/ApproveRequestByDepHead/" + hidRequestID+"/"+comment))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }
            }
            return RedirectToAction("StationeryRequests", "StationeryRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RequestHistory()
        {

            List<Request> requests = new List<Request>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "Request/GetAllRequest"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    requests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                }
            }

            ViewData["requests"] = requests;
            return View();
        }



    }
}
