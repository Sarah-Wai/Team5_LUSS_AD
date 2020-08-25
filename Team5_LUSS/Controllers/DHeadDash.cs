using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;
using Team5_LUSS.Models.ViewModels;
using Microsoft.AspNetCore.Http;


namespace Team5_LUSS.Controllers
{
    public class DHeadDashController : Controller
    {

        string api_url = "https://localhost:44312/DHeadDash/";
        string api_url_rqst = "https://localhost:44312/Request";

        

        List<TopSixRequested> highestRequestCat = new List<TopSixRequested>();
        Dictionary<string, int> requestBreakdown = new Dictionary<string, int>();
        List<DHeadMonth> deptMonthlyCost = new List<DHeadMonth>();

        public async Task<IActionResult> Index()
        {
            string name;
            using (var httpClient = new HttpClient())
            {
                int deptID = (int)HttpContext.Session.GetInt32("DeptId");
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    name = apiResponse;
                }

                using (var response = await httpClient.GetAsync(api_url + "get-top-cost-category/"+ deptID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    highestRequestCat = JsonConvert.DeserializeObject<List<TopSixRequested>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url + "get-request-breakdown/" + deptID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    requestBreakdown = JsonConvert.DeserializeObject<Dictionary<string,int>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url + "get-department-cost/" + deptID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    deptMonthlyCost = JsonConvert.DeserializeObject<List<DHeadMonth>>(apiResponse);
                }



            }
            ViewData["name"] = name;
            ViewData["highestRequestCat"] = highestRequestCat;
            ViewData["requestBreakdown"] = requestBreakdown;
            ViewData["deptMonthlyCost"] = deptMonthlyCost;





            return View("DHeadDash");
        }


    }
}