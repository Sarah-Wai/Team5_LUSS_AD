using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> CollectionPoints(int DeptID)
        {
            //Get All the Collection Points
            List<CollectionPoint> collectionPointList = new List<CollectionPoint>();
            CollectionPoint collectionPoint;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44312/CollectionPoint"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    collectionPointList = JsonConvert.DeserializeObject<List<CollectionPoint>>(apiResponse);

                }
            }
            ViewData["collectionPoints"] = collectionPointList;


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44312/CollectionPoint/" + DeptID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    collectionPoint = JsonConvert.DeserializeObject<CollectionPoint>(apiResponse);

                }


                //Get the Collection Point of that Department
                ViewData["deptCollectionPoints"] = collectionPoint;


                return View();
            }
        }
    }
}
