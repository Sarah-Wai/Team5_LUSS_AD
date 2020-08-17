
using Castle.Core.Internal;
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
        public async Task<IActionResult> collectionPoints()
        {
            //Get All the Collection Points
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
            //last object is the Department Collection Point
            ViewData["collectionPoints"] = collectionPointInfo;

            //dummy code for testing, replace with the session data
            Department d = new Department
            {
                DepartmentID = 99,
                DepartmentName = "DummyDept",
                PhoneNo = "123",
                Fax = "456",
                DepartmentCode = "DD",
                CollectionPointID = 1
            };

            dept_CP = collectionPointInfo.Where(x => x.CollectionPointID == d.CollectionPointID).FirstOrDefault();
            ViewData["dept_CollectionPoint"] = dept_CP;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> collectionPoints(int deptID, int cpID)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + deptID + "/" + cpID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }

            }
            return RedirectToAction("collectionPoints");
        }


        public async Task<IActionResult> collectionList()
        {
            List<dynamic> pd_collectionList = new List<dynamic>();
            string status = "PendingDelivery";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_rqst + "/GetItemByStatus/" + status))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pd_collectionList = JsonConvert.DeserializeObject<List<dynamic>>(apiResponse);
                }
            }

            if(pd_collectionList.IsNullOrEmpty())
            {
                ViewData["collectionRequest"] = null;
            }
            else
            {
                DateTime date = pd_collectionList.Select(x => x.collectionTime).First();
                string fm_date = date.ToString("MMMM dd, yyyy");
                ViewData["collectionTime"] = fm_date;

                //TODO: filter pd_requestList by department 
                //TODO: pass User-Dept-Collectionpoint 
                //ViewData["sessionUser"] 
                ViewData["collectionRequest"] = pd_collectionList;

            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> collectionList(List<int> acceptedQty)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(acceptedQty), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(api_url_rqst + "/" + acceptedQty, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("collectionList");
        }
    }
}


