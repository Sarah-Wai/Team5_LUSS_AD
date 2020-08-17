using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;
using System.Text;

namespace Team5_LUSS.Controllers
{
    public class DelegateController : Controller
    {
        string api_user_url = "https://localhost:44312/User"; // connect to API project Controller class
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> AddDelegate(int id)
        {
            List<User> users = new List<User>();
            id = 1;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_user_url + "/GetAllDeptUsers"+"/"+id ))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            ViewData["users"] = users;
            return View();
        }
    }
}
