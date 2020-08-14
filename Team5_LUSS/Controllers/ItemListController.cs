using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            List<ItemCategory> itemCatList = new List<ItemCategory>();
            List<Item> itemList = new List<Item>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "ItemCategory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemCatList = JsonConvert.DeserializeObject<List<ItemCategory>>(apiResponse);
                }

            }
            if (HttpContext.Session.GetString("itemListSession") == null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url + "ItemList"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        itemList = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                    }


                }
               
            }
            else
            {
                itemList = JsonConvert.DeserializeObject<List<Item>>(HttpContext.Session.GetString("itemListSession"));
            }
            ViewData["items"] = itemList;
            ViewData["itemCatList"] = itemCatList;


            return View();
        }

        public async Task<IActionResult>FindByCat(int id)
        {
            List<Item> itemList = new List<Item>();
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "ItemList/" + "GetItemListByCategoryID" + '/' + id;
                using (var response = await httpClient.GetAsync(str))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemList = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                }
            }
            ViewData["items"] = itemList;
            string itemListJson = Newtonsoft.Json.JsonConvert.SerializeObject(itemList);
            HttpContext.Session.SetString("itemListSession", itemListJson);
            return RedirectToAction("Index");
        }

    }
}
