using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
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
        string api_url_retrieval = "https://localhost:44312/Retrieval";
        public IActionResult Index()
        {
            return View();
            //return View("Disbursement_Form_View");
            //return View("Retrieval_Form");
            //return View("Disbursement_Form_Create");
        }

        public async Task<IActionResult> GetAllRetrieval()
        {
            List<Retrieval> retrievals = new List<Retrieval>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    retrievals = JsonConvert.DeserializeObject<List<Retrieval>>(apiResponse);
                }
            }

            ViewData["retrievals"] = retrievals;
            return View("Disbursement_By_Retrieval");
        }

        public async Task<IActionResult> RetrievalForm()
        {
            List<dynamic> items = new List<dynamic>();
            string status = "Approved";

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_retrieval + "/" + status))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<dynamic>>(apiResponse);
                }
            }

            ViewData["items"] = items;
            return View("Retrieval_Form");
        }

        public async Task<IActionResult> RetrievalAllocation(int id)
        {
            List<dynamic> items = new List<dynamic>();
            Retrieval retrieval = new Retrieval();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url_retrieval + "/retrievalID/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<dynamic>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url_retrieval + "/retrieveID/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    retrieval = JsonConvert.DeserializeObject<Retrieval>(apiResponse);
                }
            }

            ViewData["items"] = items;
            ViewData["retrieval"] = retrieval;
            return View("Disbursement_By_Retrieval");
        }


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


        public async Task<IActionResult> Create(int id)
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
            return View("Disbursement_Form_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(List<int> fulfillQty, List<String> itemID, DateTime collectionTime)
        {
            return RedirectToAction("View");
        }
    }
}