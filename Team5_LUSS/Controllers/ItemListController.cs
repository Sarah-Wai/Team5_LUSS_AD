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
            List<Item> itemList = new List<Item>();                       
            List<ItemCategory> itemCatList = new List<ItemCategory>();   //for DropDownList
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "ItemCategory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemCatList = JsonConvert.DeserializeObject<List<ItemCategory>>(apiResponse);
                }

            }

            //check whether we have data inside Session first
            //if null, we load full item images
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

            //if not null, we will only show what is inside Session
            else
            {
                itemList = JsonConvert.DeserializeObject<List<Item>>(HttpContext.Session.GetString("itemListSession"));
            }
            ViewData["items"] = itemList;
            ViewData["itemCatList"] = itemCatList;

            
            return View();
        }

        //FindItemByCategory action using Session to store data
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
            string itemListJson = Newtonsoft.Json.JsonConvert.SerializeObject(itemList);    //store Json string value inside Session
            HttpContext.Session.SetString("itemListSession", itemListJson);                 //set Session String as (key,value) format
            return RedirectToAction("Index");
        }

    }
}
