
using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Team5_LUSS.Models;

namespace Team5_LUSS.Controllers
{

    public class CollectionController : Controller
    {

        string api_url = "https://localhost:44312/CollectionPoint";
        string api_url_rqst = "https://localhost:44312/Request";

        public async Task<IActionResult> collectionPoints(CollectionPoint newCP)
        {

            int user_CPId = (int)HttpContext.Session.GetInt32("CPId");
            List<CollectionPoint> collectionPointInfo = new List<CollectionPoint>();
            CollectionPoint dept_CP = new CollectionPoint();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    collectionPointInfo = JsonConvert.DeserializeObject<List<CollectionPoint>>(apiResponse);
                }
            }

            ViewData["collectionPoints"] = collectionPointInfo;
            if (newCP.CollectionPointID == 0)
            {
                dept_CP = collectionPointInfo.Where(x => x.CollectionPointID == user_CPId).FirstOrDefault();
            }
            else
            {
                dept_CP = newCP;
            }
            ViewData["dept_CollectionPoint"] = dept_CP;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> collectionPoints(int cpID)
        {
            CollectionPoint dept_CP = new CollectionPoint();
            int deptID = (int)HttpContext.Session.GetInt32("DeptId");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + deptID + "/" + cpID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dept_CP = JsonConvert.DeserializeObject<CollectionPoint>(apiResponse);
                }
            }
            TempData["AlertMessage"] = "Updated";
            return RedirectToAction("collectionPoints", dept_CP);
        }


        public async Task<IActionResult> collectionList(int retrievalID)
        {
            List<dynamic> pd_collectionList = new List<dynamic>();
            string status = "PendingDelivery";
            List<Request> pendingDeliveryRequests = new List<Request>();
            Dictionary<int, DateTime> retrievalID_CollectionTime = new Dictionary<int, DateTime>();
            CollectionPoint cp = new CollectionPoint();
            int user_CPId = (int)HttpContext.Session.GetInt32("CPId");
            int deptID = (int)HttpContext.Session.GetInt32("DeptId");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_rqst + "/GetRequestByStatusByDept/" + status + "/" + deptID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pendingDeliveryRequests = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url + "/" + user_CPId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cp = JsonConvert.DeserializeObject<CollectionPoint>(apiResponse);
                }
            }

            foreach (var r in pendingDeliveryRequests)
            {
                int? key = r.RetrievalID;
                DateTime value = r.CollectionTime;
                if (!retrievalID_CollectionTime.ContainsKey((int)key))
                {
                    retrievalID_CollectionTime.Add((int)key, value);
                }
            }

            if (retrievalID != 0)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url_rqst + "/GetItemByStatus/" + status + "/" + retrievalID + "/" + deptID))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        pd_collectionList = JsonConvert.DeserializeObject<List<dynamic>>(apiResponse);
                    }
                }
            }

            if (pd_collectionList.IsNullOrEmpty() || retrievalID_CollectionTime.IsNullOrEmpty())
            {
                ViewData["collectionRequest"] = null;
            }
 
            ViewData["collectionRequest"] = pd_collectionList;
            ViewData["retrieval_time"] = retrievalID_CollectionTime;
            ViewData["collectionPoint"] = cp;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> collectionList(List<int> acceptedQty, int retrievalID)
        {
            int deptID = (int)HttpContext.Session.GetInt32("DeptId");
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(acceptedQty), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(api_url_rqst + "/" + acceptedQty +"/" + retrievalID + "/" + deptID, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            TempData["AlertMessage"] = "Success";
            return RedirectToAction("collectionList");
        }
    }
}


