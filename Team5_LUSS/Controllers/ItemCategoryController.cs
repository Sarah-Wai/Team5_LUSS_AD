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
    public class ItemCategoryController : Controller
    {
        string api_url = "https://localhost:44312/ItemCategory"; // connect to API project Controller class
        public async Task<IActionResult> ItemCategory()
        {
            List<ItemCategory> catList = new List<ItemCategory>(); // create a new list with objects of "ItemCatergory"
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url)) // connect to call api
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    catList = JsonConvert.DeserializeObject<List<ItemCategory>>(apiResponse); // convert the packets from https link to the object
                }
            }
            ViewData["products"] = catList;
            return View();
          
        }

        public ViewResult GetItemCategory() => View();

        public IActionResult CollectionPoints()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetItemCategory(int id)
        {
            ItemCategory itemCategory = new ItemCategory();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemCategory = JsonConvert.DeserializeObject<ItemCategory>(apiResponse);
                }
            }
            return View(itemCategory);
        }


        public ViewResult AddItemCategory() => View();

        [HttpPost]
        public async Task<IActionResult> AddItemCategory(ItemCategory reservation)
        {
            ItemCategory receivedItemCategory = new ItemCategory();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(api_url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedItemCategory = JsonConvert.DeserializeObject<ItemCategory>(apiResponse);
                }
            }
            ViewData["products"] = receivedItemCategory;
            return View(receivedItemCategory);
        }
        public async Task<IActionResult> UpdateItemCategory(int id)
        {
            ItemCategory itemCategory = new ItemCategory();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url+"/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemCategory = JsonConvert.DeserializeObject<ItemCategory>(apiResponse);
                }
            }
            return View(itemCategory);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItemCategory(ItemCategory itemCategory)
        {
            ItemCategory receivedItemCategory = new ItemCategory();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(itemCategory.CategoryID.ToString()), "CategoryID");
                content.Add(new StringContent(itemCategory.CategoryName), "CategoryName");

                using (var response = await httpClient.PutAsync(api_url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedItemCategory = JsonConvert.DeserializeObject<ItemCategory>(apiResponse);
                }
            }
            return View(receivedItemCategory);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItemCategory(int ItemCategoryID)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(api_url+"/" + ItemCategoryID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("ItemCategory");
        }
    }
}
