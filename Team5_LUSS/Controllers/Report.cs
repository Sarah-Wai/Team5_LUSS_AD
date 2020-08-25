using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;
using Team5_LUSS.Models.ViewModels;

namespace Team5_LUSS.Controllers
{
    public class Report : Controller
    {
        
        string api_url = "https://localhost:44312/Report/";

        List<string> months = new List<string> 
        { DateTime.Now.AddMonths(-2).ToString("MMM"), DateTime.Now.AddMonths(-1).ToString("MMM"), DateTime.Now.ToString("MMM") };

        List<CategoryActorSum> departmentMonth = new List<CategoryActorSum>();
        List<CategoryActorSum> supplierMonth = new List<CategoryActorSum>();


        public async Task<IActionResult> Index()
        {
            string name;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    name = apiResponse;
                }

                using (var response = await httpClient.GetAsync(api_url + "get-month-on-month-dept"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    departmentMonth = JsonConvert.DeserializeObject<List<CategoryActorSum>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url + "get-month-on-month-supplier"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    supplierMonth = JsonConvert.DeserializeObject<List<CategoryActorSum>>(apiResponse);
                }


            }


            ViewData["departmentMonth"] = departmentMonth;
            ViewData["months"] = months;

            ViewData["supplierMonth"] = supplierMonth;




            return View("Report");
        }


    }
}