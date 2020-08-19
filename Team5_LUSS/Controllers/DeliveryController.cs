using Castle.Core.Internal;
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
        string api_url_rqst_detail = "https://localhost:44312/RequestDetails";
        string api_url_user = "https://localhost:44312/User";

        #region DeliveryView_ByRequest
        public async Task<IActionResult> ConfirmDelivery(string status)
        {
            List<Request> allRequests = new List<Request>();

            if (status == null)
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


            allRequests = filterForStoreClerkView(allRequests);
            ViewData["allRqt"] = allRequests;
            return View();
        }
        #endregion

        #region DeliveryView_ByDepartment
        public async Task<IActionResult> DeptConfirmDelivery(string status)
        {
            List<Request> dept_Request = new List<Request>();
            //create DeptCode - DeptName dictionary
            Dictionary<string, string> status_byDept = new Dictionary<string, string>();


            if (status != null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url_rqst + "/GetRequestByStatus/" + status))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        dept_Request = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                    }
                }
            }
            else
            {
                //if status = null, get all the requests
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url_rqst + "/getAllRequest"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        dept_Request = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                    }
                }

            }
            dept_Request = filterForStoreClerkView(dept_Request);

            foreach (Request r in dept_Request)
            {
                string key = r.RequestByUser.Department.DepartmentName;
                string value = r.RequestByUser.Department.DepartmentCode;

                if (!status_byDept.ContainsKey(key))
                    status_byDept.Add(key, value);
            }

            if (status_byDept.IsNullOrEmpty())
            {
                ViewData["status_byDept"] = null;
            }


            ViewData["status_byDept"] = status_byDept;
            return View();
        }
        #endregion
        private List<Request> filterForStoreClerkView(List<Request> allRequests)
        {
            List<Request> new_allRequests = allRequests.Where(x => x.RequestStatus == EOrderStatus.PendingDelivery
            || x.RequestStatus == EOrderStatus.Completed
            || x.RequestStatus == EOrderStatus.Received).ToList();

            return new_allRequests;
        }


        #region DeliveryView_DisbursementDetails
        [HttpPost]
        public async Task<IActionResult> DisbursementDetails(int reqID, string status)
        {
            List<RequestDetails> requestDetails = new List<RequestDetails>();
            Request request = new Request();
            User representative = new User();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_rqst_detail + "/get-by-request/" + reqID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    requestDetails = JsonConvert.DeserializeObject<List<RequestDetails>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url_rqst + "/get-request/" + reqID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }

                int deptID = request.RequestByUser.DepartmentID;
                using (var response = await httpClient.GetAsync(api_url_user + "/get-representative/" + deptID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    representative = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }

            ViewData["deptRep"] = representative;
            ViewData["request"] = request;
            ViewData["requestDetails"] = requestDetails;

            return View("Disbursement_Form_View");
        }

        #endregion

        #region Disbursement_FinalConfirm_Deny
        public async Task<IActionResult> FinalActionByStore(string actionTaken, int requestID)
        {
            //change status to pending delivery
            using (var httpClient = new HttpClient())
            {
                if (actionTaken == "deny")
                {
                    using (var response = await httpClient.GetAsync(api_url_rqst + "/deny/" + requestID))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
                else{
                    //change status to Completed and create discrepancy order
                    using (var response = await httpClient.GetAsync(api_url_rqst + "/complete/" + requestID))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return RedirectToAction("ConfirmDelivery");
        }
        #endregion
    }
}
