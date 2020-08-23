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
            int id = 1; //depID
            List<Request> requests = new List<Request>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url+ "/getAllRequest/"+id))
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
                using (var response = await httpClient.GetAsync(api_url + "/ApproveRequestByDepHead/"+id+"/"+status+ "/"+comment))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                   
                }
            }

            return RedirectToAction("StationeryRequests");
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
            int userId = (int)HttpContext.Session.GetInt32("UserID");
            List<Request> requests = new List<Request>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "Request/GetRequestByEmpId/" + userId))
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
            ViewData["requests"] = requests;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewRequestDetail(int id)
        {
            //if(TempData["AlertMessage"] != null)
            //{
            //    TempData["AlertMessage"] = null;
            //}
            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "Request/GetReqById/" + id;
                using (var response = await httpClient.GetAsync(str))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }
            }
            if(request != null)
            {
                List<RequestDetails> newReqDetail = new List<RequestDetails>();
                foreach (var r in request.RequestDetails)
                {
                    if (r.isActive == true)
                    {
                        newReqDetail.Add(r);
                    }
                }
                request.RequestDetails = newReqDetail;
            }

            ViewData["request"] = request;

            return View();
        }

        public async Task<IActionResult> CancelRequest(int reqId)
        {
            int userId = (int)HttpContext.Session.GetInt32("UserID");
            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "Request/CancelRequest/" + reqId;
                using (var response = await httpClient.GetAsync(str))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }
            }
            ViewData["request"] = request;
            if (request != null)
            {
                TempData["AlertMessage"] = "Cancelled";
            }
            return RedirectToAction("RequestHistory", new { id = userId });
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
                }
                else if (items.Key.Contains("reqID"))
                {
                    reqID = items.Value;
                }
            }
            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "Request/GetReqById/" + Int32.Parse(reqID);
                using (var response = await httpClient.GetAsync(str))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }
            }

            for (int i = 0; i < itemIds.Length; i++)
            {
               
                foreach (var requestDetail in request.RequestDetails)
                {
                    if (requestDetail.ItemID == Int32.Parse(itemIds[i]))
                    {
                        requestDetail.RequestQty = Int32.Parse(quantities[i]);
                        requestDetail.isActive = true;
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
            if (returnData)
            {

                TempData["AlertMessage"] = "Success";
            }
            else
            {
                return RedirectToAction("ViewRequestDetail", new { id = reqID });
            }

            return RedirectToAction("ViewRequestDetail", new { id = reqID });
        }

        public async Task<IActionResult> RemoveRequestedItem(int reqId, int reDetailId)
        {
            bool isRemoved = false;
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "Request/RemoveRequestedItem/" + reqId + "/" + reDetailId;
                using (var response = await httpClient.GetAsync(str))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    isRemoved = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }
            
            if (isRemoved)
            {
                TempData["AlertMessage"] = "Removed";
            }

            return RedirectToAction("ViewRequestDetail", new { id = reqId });
        }
    }
}
