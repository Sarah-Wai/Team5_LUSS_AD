using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;

namespace Team5_LUSS.Controllers
{
    public class DisbursementController : Controller
    {
        string api_url = "https://localhost:44312/Request/";
        string api_url_user = "https://localhost:44312/User/";
        string api_url_requestDetails = "https://localhost:44312/RequestDetails/";
        string api_url_retrieval = "https://localhost:44312/Retrieval";

        public async Task<IActionResult> RetrievalForm()
        {
            int userId = (int)HttpContext.Session.GetInt32("UserID");
            List<dynamic> items = new List<dynamic>();
            string status = "Approved";

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_retrieval + "/byStatus/" + status))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<dynamic>>(apiResponse);
                }
            }

            if(items == null || items.Count == 0)
            {
                TempData["noItem"] = "There are no pending request to disburse.";
                return RedirectToAction("ManageDisbursement");
            }

            ViewData["items"] = items;
            ViewData["userId"] = userId;
            return View("Retrieval_Form");
            
        }

        [HttpPost]
        public async Task<IActionResult> CancelRetrieval(int retrievalID)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_retrieval + "/retrievalId/" + retrievalID))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("ManageDisbursement");
        }

        [HttpPost]
        public async Task<IActionResult> CompleteRetrieval(List<int> retrievedQty, int retrievalId, string collectionDate, int id)
        {
            int fromID = (int)HttpContext.Session.GetInt32("UserID");
            List<User> toID = new List<User>();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(retrievedQty), Encoding.UTF8, "application/json");
                using (var reponse = await httpClient.PostAsync(api_url_retrieval + "/allocate/" + retrievedQty + "/" + retrievalId + "/" + collectionDate + "/" + id, content))
                {
                    string apiResponse = await reponse.Content.ReadAsStringAsync();
                    toID = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }

            foreach(User u in toID)
            {
                NotificationController.ReadyForCollection(fromID, u.UserID);
            }
                return RedirectToAction("ManageDisbursement");

        }

        public async Task<IActionResult> GetDistributionDetailsById(int id)
        {
            List<dynamic> requests = new List<dynamic>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_retrieval + "/itemID/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    requests = JsonConvert.DeserializeObject<List<dynamic>>(apiResponse);
                }
            }

            ViewData["requests"] = requests;
            return View("Retrieval_Form");
        }


        #region Manage Disbursement
        [HttpGet]
        public async Task<IActionResult> ManageDisbursement(string type)
        {

            List<Request> requests = new List<Request>();

            using (var httpClient = new HttpClient())
            {
                if(type == null)
                {
                    using (var response = await httpClient.GetAsync(api_url + "get-approved-request/"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        requests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                    }
                }

                else
                {
                    using (var response = await httpClient.GetAsync(api_url + "byType/" + type))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        requests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                    }
                }
                
            }

            if(requests.Count == 0)
            {
                TempData["noItem"] = "There are no pending request to disburse.";
            }

            ViewData["requests"] = requests;
            return View("Disbursement_Manage");
        }

        #endregion

        #region DisbursementFormView
        public async Task<IActionResult> View(int id)
        {
            Request request = new Request();
            User deptRep = new User();
            List<RequestDetails> reqItems = new List<RequestDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "get-request/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url_user + "get-representative/" + request.RequestByUser.DepartmentID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    deptRep = JsonConvert.DeserializeObject<User>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url_requestDetails + "get-by-request/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reqItems = JsonConvert.DeserializeObject<List<RequestDetails>>(apiResponse);
                }
            }
            ViewData["request"] = request;
            ViewData["deptRep"] = deptRep;
            ViewData["reqItems"] = reqItems;
            return View("Disbursement_Form_View");
        }
        #endregion

        #region DisburseByRequest
        public async Task<IActionResult> DisburseByRequest(int id)
        {
            Request request = new Request();
            User deptRep = new User();
            List<RequestDetails> reqItems = new List<RequestDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "get-request/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url_user + "get-representative/" + request.RequestByUser.DepartmentID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    deptRep = JsonConvert.DeserializeObject<User>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url_requestDetails + "get-by-request/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reqItems = JsonConvert.DeserializeObject<List<RequestDetails>>(apiResponse);
                }
            }
            int userId = (int)HttpContext.Session.GetInt32("UserID"); 
            ViewData["userId"] = userId;
            ViewData["request"] = request;
            ViewData["deptRep"] = deptRep;
            ViewData["reqItems"] = reqItems;
            return View("Disbursement_Form_Create");
        }

        [HttpPost]
        public async Task<IActionResult> DisburseByRequest(int id, int userId, string collectionTime, List<int> fulfillQty)
        {
            int fromID = (int)HttpContext.Session.GetInt32("UserID");
            int toID;
            using (var httpClient = new HttpClient())
            {
               
                StringContent content = new StringContent(JsonConvert.SerializeObject(fulfillQty), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(api_url + "disburse-by-request/" + id + "/" + userId + "/" + collectionTime + "/" + fulfillQty, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    toID = JsonConvert.DeserializeObject<int>(apiResponse);
                }

                NotificationController.ReadyForCollection(fromID, toID);

            }
            return RedirectToAction("ManageDisbursement");
        }
        #endregion
    }
}