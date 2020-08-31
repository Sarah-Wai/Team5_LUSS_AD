using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
                    using (var response = await httpClient.GetAsync(api_url_rqst + "/get-all-requests"))
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
            ViewData["allRqt"] = allRequests;
            return View();
        }
        #endregion

        #region DeliveryView_ByDepartment
        public async Task<IActionResult> DeptConfirmDelivery(string status, string deptName)
        {
            List<Request> dept_Request = new List<Request>();
            //create DeptCode - DeptName dictionary
            Dictionary<string, string> status_byDept = new Dictionary<string, string>();

            //1st get, show all retrieval items

            //filter by status
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
                    using (var response = await httpClient.GetAsync(api_url_rqst + "/get-all-requests"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        dept_Request = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                    }
                }

            }
            dept_Request = filterForStoreClerkView(dept_Request);

            //prepare Department Info
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


            //filter by departmentName
            if (deptName != null)
            {
                dept_Request = dept_Request.Where(x => x.RequestByUser.Department.DepartmentName == deptName).ToList();
            }

            //filter by retrieval ID
            var iter = (from r in dept_Request
                        select new
                        {
                            RetrievalID = r.RetrievalID,
                            DepartmentCode = r.RequestByUser.Department.DepartmentCode,
                            DepartmentName = r.RequestByUser.Department.DepartmentName,
                            DepartmentID = r.RequestByUser.Department.DepartmentID,
                            Time = r.CollectionTime.ToString("MMMM dd, yyyy"),
                            Status = r.RequestStatus.ToString()

                        }).Distinct().ToList();
            List<dynamic> x = new List<dynamic> {};


            foreach (var r in iter)
            {
                dynamic return_value = new System.Dynamic.ExpandoObject();
                return_value.RetrievalID = r.RetrievalID;
                return_value.DepartmentCode = r.DepartmentCode;
                return_value.DepartmentName = r.DepartmentName;
                return_value.DepartmentID = r.DepartmentID;
                return_value.Time = r.Time;
                return_value.Status = r.Status;
                x.Add(return_value);
            }


            ViewData["deptName"] = deptName;
            ViewData["dept_Requests"] = x;
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
        public async Task<IActionResult> DisbursementDetails(int reqID)
        {
            List<RequestDetails> requestDetails = new List<RequestDetails>();
            Request request = new Request();
            User representative = new User();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_rqst_detail + "/get-by-request-lowdata/" + reqID))
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

            return View("Disbursement_Form_View_Rqst");
        }

        [HttpPost]
        public async Task<IActionResult> DisbursementDetailsByDept(int retrievalID, string status, string deptID)
        {
            List<dynamic> pd_collectionList = new List<dynamic>();
            User representative = new User();

            //get the list of items based on retrieval ID & status
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_rqst + "/GetItemByStatus/" + status + "/" + retrievalID + "/" + deptID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pd_collectionList = JsonConvert.DeserializeObject<List<dynamic>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url_user + "/get-representative/" + deptID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    representative = JsonConvert.DeserializeObject<User>(apiResponse);
                }

            }

            ViewData["deptRep"] = representative;
            ViewData["retrievalDetails"] = pd_collectionList;
            ViewData["status"] = status;

            return View("Disbursement_Form_View _Dept");
        }


        #endregion




        #region Disbursement_FinalConfirm_Deny/Complete
        public async Task<IActionResult> FinalActionByStore(string actionTaken, int requestID)
        {
            int userId = (int)HttpContext.Session.GetInt32("UserID");

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
                else
                {
                    //change status to Completed and create discrepancy order
                    using (var response = await httpClient.GetAsync(api_url_rqst + "/complete/" + requestID + "/" + userId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            if(actionTaken == "deny")
            {
                TempData["AlertMessage"] = "Deny";
            }
            else
            {
                TempData["AlertMessage"] = "Complete";
            }
            
            return RedirectToAction("ConfirmDelivery");
        }



        public async Task<IActionResult> FinalActionByStoreDept(string actionTaken, List<int> requestID)
        {
            List<int> requestIDList = requestID.Distinct().ToList();
            foreach (var id in requestIDList)
            {
                await FinalActionByStore(actionTaken, id);
            }
            if (actionTaken == "deny")
            {
                TempData["AlertMessage"] = "Deny";
            }
            else
            {
                TempData["AlertMessage"] = "Complete";
            }
            return RedirectToAction("DeptConfirmDelivery");
        }
        #endregion
    }
}
