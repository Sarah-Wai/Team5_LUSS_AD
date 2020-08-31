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
    public class SupDashController : Controller
    {

        string api_url = "https://localhost:44312/SupDash/";

        

        List<CategoryActorSum> departmentCategory = new List<CategoryActorSum>();
        List<CategoryActorSum> supplierCategory = new List<CategoryActorSum>();

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

                using (var response = await httpClient.GetAsync(api_url + "get-by-department-category"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    departmentCategory = JsonConvert.DeserializeObject<List<CategoryActorSum>>(apiResponse);
                }
                using (var response = await httpClient.GetAsync(api_url + "get-by-supplier-category"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    supplierCategory = JsonConvert.DeserializeObject<List<CategoryActorSum>>(apiResponse);
                }

            }

            ViewData["departmentCategory"] = departmentCategory;
            ViewData["supplierCategory"] = supplierCategory;

            return View("SupDash");
        }


    }
}