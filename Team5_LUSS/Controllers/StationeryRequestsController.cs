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
        string api_url = "https://localhost:44312/request";

        [HttpGet]
        public async Task<IActionResult> StationeryRequests()
        {
           int user_DEPId = (int)HttpContext.Session.GetInt32("DeptId");
         
            List<Request> requests = new List<Request>();
            List<RequestDetails> requestDetil = new List<RequestDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url+ "/getAllRequestByDepID/" + user_DEPId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    requests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                }
              
            }

            ViewData["requests"] = requests;
            return View();
        }

        [HttpGet]
        public async Task<string> ApproveRequestByDepHead(int id,int status, string comment)
        {
            if (comment == "" || comment=="null" || comment==null)
            {
                comment = "-";
            }
            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/ApproveRequestByDepHead/"+id+"/"+status+ "/"+comment))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                   
                }
            }
            return "Success";

            //return RedirectToAction("StationeryRequests");
        }

        [HttpGet]
        public async Task<List<RequestDetails>> GetRequestDetail(int id)
        {
           
            List<RequestDetails> request_detis = new List<RequestDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44312/RequestDetails/get-by-request-lowdata/" + id ))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request_detis = JsonConvert.DeserializeObject<List<RequestDetails>>(apiResponse);

                }
            }

            return request_detis;
        }

        //Nang Sandar: getting request order list
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
                using (var response = await httpClient.GetAsync(api_url + "/GetRequestByEmpId/" + userId))
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

        //Nang Sandar: Request Detail View
        [HttpGet]
        public async Task<IActionResult> ViewRequestDetail(int id)
        {

            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "/GetReqById/" + id;
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

        //Nang Sandar: for Request Cancel function at 'Pending' state
        public async Task<IActionResult> CancelRequest(int reqId)
        {
            int userId = (int)HttpContext.Session.GetInt32("UserID");
            Request request = new Request();
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "/CancelRequest/" + reqId;
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

        //Nang Sandar: for update Request function at 'Pending' state
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
                string str = api_url + "/GetReqById/" + Int32.Parse(reqID);
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
                using (var response = await httpClient.PostAsync(api_url + "/UpdateRequestDetail", content))
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

        //Nang Sandar: for Remove item function for requested order
        public async Task<IActionResult> RemoveRequestedItem(int reqId, int reDetailId)
        {
            bool isRemoved = false;
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "/RemoveRequestedItem/" + reqId + "/" + reDetailId;
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
