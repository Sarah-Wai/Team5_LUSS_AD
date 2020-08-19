using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Team5_LUSS.Controllers
{
    public class ClerkDashController : Controller
    {

        string api_url = "https://localhost:44312/ClerkDash";
        
        public async Task<IActionResult> Index()
        {
            string name;
            string placeholder = "Placeholder";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    name = apiResponse;
                }
            }

            if (name.IsNullOrEmpty())
            {
                ViewData["name"] = placeholder;
            }
            else
            {
               
                ViewData["name"] = name;

            }

            return View("ClerkDash");
        }


    }

}