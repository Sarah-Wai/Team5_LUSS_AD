using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Team5_LUSS.Controllers
{
    public class RepresentativeController : Controller
    {
        string api_user_url = "https://localhost:44312/User/get-none-delegate-lower"; // connect to API project Controller class
        string api_representative_url = "https://localhost:44312/Representative";

        [HttpGet]
        public async Task<IActionResult> AssignRepresentative(int id,string msg)
        {
            List<User> users = new List<User>();
            id = (int)HttpContext.Session.GetInt32("DeptId");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_user_url + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            if (msg != "")
            {
                ViewData["DeleteMsg"] = msg;
            }

            ViewData["users"] = users;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddRepresentative(int id)
        {
            string apiResponse = "";
            int depID = (int)HttpContext.Session.GetInt32("DeptId");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_representative_url + "/" + id+ "/"+true+"/"+depID))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            string msg = "Assign Successfully!";
            return RedirectToAction("AssignRepresentative", new { id = 1, msg=msg }) ;
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRepresentative(int id)
        {
            string apiResponse = "";
            int depID = (int)HttpContext.Session.GetInt32("DeptId");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_representative_url + "/" + id + "/" + false + "/" + depID))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            string msg = "Remove Successfully!";
            return RedirectToAction("AssignRepresentative", new { id = 1, msg = msg });
        }

    }

}
