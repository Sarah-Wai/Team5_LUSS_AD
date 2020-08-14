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
    public class ItemListController : Controller
    {
        string api_url = "https://localhost:44312/";

        public async Task<IActionResult> Index()
        {
            List<Item> itemList = new List<Item>();
            List<ItemCategory> itemCatList = new List<ItemCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "ItemList"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemList = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url + "ItemCategory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemCatList = JsonConvert.DeserializeObject<List<ItemCategory>>(apiResponse);
                }

            }
            ViewData["items"] = itemList;
            ViewData["itemCatList"] = itemCatList;
            return View();
        }
    }
}
