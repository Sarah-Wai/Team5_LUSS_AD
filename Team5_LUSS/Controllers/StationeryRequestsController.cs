using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;
using System.Text;
using Microsoft.AspNetCore.Http;
using static Team5_LUSS.Models.Status;

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
                using (var response = await httpClient.GetAsync(api_url + "Request/GetAllRequest"))
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
                using (var response = await httpClient.GetAsync(api_url + "Request/ApproveRequestByDepHead/" + hidRequestID + "/" + comment))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }
            }
            return RedirectToAction("StationeryRequests", "StationeryRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RequestHistory(string status)
        {
            if (status == null)
            {
                string selectedStatusSession = HttpContext.Session.GetString("selectedStatus");
                if (selectedStatusSession == null)
                {
                    status = "All";
                }
                else
                {
                    status = selectedStatusSession;
                }

            }
            int empId = 2;
            List<Request> requests = new List<Request>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "Request/GetRequestByEmpId/" + empId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    requests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                    if (status != "All")
                    {
                        EOrderStatus e = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
                        requests.RemoveAll(x => x.RequestStatus != e);
                    }

                }
            }
            HttpContext.Session.SetString("selectedStatus", status.ToString());
            ViewData["requests"] = requests.OrderBy(r=> r.RequestID).ToList();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewRequestDetail(int id)
        {
            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "Request/GetById/" + id;
                using (var response = await httpClient.GetAsync(str))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }
            }
            
            ViewData["request"] = request;
            
            return View();
        }
        public async Task<IActionResult> CancelRequest(int id)
        {
            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "Request/CancelRequest/" + id;
                using (var response = await httpClient.GetAsync(str))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }
            }
            ViewData["request"] = request;
            if(request  != null)
            {
                TempData["AlertMessage"] = "Cancelled";
            }
            return RedirectToAction("ViewRequestDetail", new { id = id });
        }
        public async Task<IActionResult> UpdateRequestDetail()
        {
            // formData ---> getting string value from View
            var FormData = HttpContext.Request.Form;
            //storing data in array type for two data entry
            string[] itemIds = null;
            string[] quantities = null;
            string reqID = "";
            foreach (var items in FormData)
            {
                //check each data using 'Contain" and assigning respestively
                if (items.Key.Contains("itemId"))
                {
                    itemIds = items.Value;
                }
                else if (items.Key.Contains("quantity"))
                {
                    quantities = items.Value;
                }else if (items.Key.Contains("reqID"))
                {
                    reqID = items.Value;
                }
            }
            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "Request/GetById/" + Int32.Parse(reqID);
                using (var response = await httpClient.GetAsync(str))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }
            }
           
            for (int i = 0; i < itemIds.Length; i++)
            {
                var itemid = Int32.Parse(itemIds[i]);
                foreach (var requestDetail in request.RequestDetails)
                {
                    if (requestDetail.ItemID == Int32.Parse(itemIds[i]))
                    {
                        requestDetail.RequestQty = Int32.Parse(quantities[i]);
                        
                    }
                }
                
            }
            string requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(request);

            bool returnData = false;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(requestJson), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(api_url + "Request/UpdateRequestDetail", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    returnData = JsonConvert.DeserializeObject<Boolean>(apiResponse);
                }

            }
            if(returnData)
            {

                TempData["AlertMessage"] = "Success";
            }
            else
            {
                return RedirectToAction("ViewRequestDetail", new { id = reqID });
            }
           
            return RedirectToAction("ViewRequestDetail", new { id = reqID });
        }
    }
}
