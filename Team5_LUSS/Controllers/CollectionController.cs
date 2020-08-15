
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Team5_LUSS.Models;

namespace Team5_LUSS.Controllers
{

    public class CollectionController : Controller
    {

        string api_url = "https://localhost:44312/CollectionPoint";
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
            return View();
        }
    }
}
