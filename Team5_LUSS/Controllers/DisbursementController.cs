using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        public IActionResult Index()
        {
            return View();
            //return View("Disbursement_Form_View");
            //return View("Retrieval_Form");
            //return View("Disbursement_Form_Create");
        }

    
        public async Task<IActionResult> View(int id)
        {
            Request request = new Request();
            User deptRep = new User();
            List<RequestDetails> reqItems = new List<RequestDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<Request>(apiResponse);
                }

                //using (var response = await httpClient.GetAsync(api_url_user + 1))
                using (var response = await httpClient.GetAsync(api_url_user + request.RequestByUser.DepartmentID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    deptRep = JsonConvert.DeserializeObject<User>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url_requestDetails + id))
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
    }
}