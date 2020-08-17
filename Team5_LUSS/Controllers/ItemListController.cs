﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;
using Team5_LUSS.Models.ViewModels;

namespace Team5_LUSS.Controllers
{
    public class ItemListController : Controller
    {
        string api_url = "https://localhost:44312/";

        public async Task<IActionResult> Index()
        {
            List<Item> itemList = new List<Item>();                       
            List<ItemCategory> itemCatList = new List<ItemCategory>();
            ItemCategory defaultItem = new ItemCategory();

            //we set the new default value "ALL" for dropdownlist
            defaultItem.CategoryID = 0;
            defaultItem.CategoryName = "All";
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "ItemCategory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemCatList = JsonConvert.DeserializeObject<List<ItemCategory>>(apiResponse);
                    itemCatList.Add(defaultItem);                                              //add defaultItem in itemCatList to pass the value to View
                    itemCatList = itemCatList.OrderBy(o => o.CategoryName).ToList();           //sort the item name to get "ALL" first
                }

            }

            //check whether we have data inside Session first
            //if null or CategoryID '0', we load full item list
            if (HttpContext.Session.GetString("itemListSession") == null || (HttpContext.Session.GetString("selectedCat") == "0" && HttpContext.Session.GetString("itemListSession") == null))
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(api_url + "ItemList"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        itemList = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                        HttpContext.Session.SetString("selectedCat", "0");              
                    }
                }           
            }
            //if not null, we will only show those items list which is inside Session
            else
            {

                itemList = JsonConvert.DeserializeObject<List<Item>>(HttpContext.Session.GetString("itemListSession"));
            }
            ViewData["items"] = itemList;
            ViewData["itemCatList"] = itemCatList;
 
            return View();
        }

        //FindItemByCategory action for dropdownList using Session to store data
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
            HttpContext.Session.SetString("selectedCat", id.ToString());                    
            return RedirectToAction("Index");
        }

        //getting param ID and ItemName from dropdownList and search box by View side
        public async Task<IActionResult> FindByCatIDAndName(int id, string name)
        {
            List<Item> itemList = new List<Item>();
            using (var httpClient = new HttpClient())
            {
                string str = api_url + "ItemList/" + "FindByCatTDAndCatName" + '/' + id + '/' + name;
                using (var response = await httpClient.GetAsync(str))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    itemList = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                }
            }
            ViewData["items"] = itemList;
            string itemListJson = Newtonsoft.Json.JsonConvert.SerializeObject(itemList);    //store Json string value inside Session
            HttpContext.Session.SetString("itemListSession", itemListJson);                 //set Session String as (key,value) format
            HttpContext.Session.SetString("selectedCat", id.ToString()); ;
            return RedirectToAction("Index");
        }

        //Add to Cart 
        public async Task<IActionResult> AddToCart(int itemId, string qty)
        {
            List<AddToCartItem> allItem = new List<AddToCartItem>();
            AddToCartItem addToCartItem = new AddToCartItem();
            List<AddToCartItem> addToCartItemList = new List<AddToCartItem>();

            using (var httpClient = new HttpClient())
            {
                string str = api_url + "ItemList";              
                using (var response = await httpClient.GetAsync(str))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    allItem = JsonConvert.DeserializeObject<List<AddToCartItem>>(apiResponse);
                   
                }
                
            }
            if(HttpContext.Session.GetString("addedItemSession")== null){
                addToCartItem = allItem.Where(x => x.ItemID.Equals(itemId)).FirstOrDefault();
                addToCartItem.SelectedQty = Int32.Parse(qty);
                addToCartItemList.Add(addToCartItem);
                string addedItemJson = Newtonsoft.Json.JsonConvert.SerializeObject(addToCartItemList);
                HttpContext.Session.SetString("addedItemSession", addedItemJson);
            }
            else
            {

                addToCartItemList = JsonConvert.DeserializeObject<List<AddToCartItem>>(HttpContext.Session.GetString("addedItemSession"));

                List<AddToCartItem> addToCartUpdatedItemList = new List<AddToCartItem>();

                int index = -1;
                for(int i = 0; i < addToCartItemList.Count; i++)
                {
                    if(addToCartItemList[i].ItemID == itemId)
                    {
                        //here we go - found existing item in the list , so we just replace index variable with it's index number from loop 
                        index = i;
                    }
                   
                }
                if(index != -1)
                {
                    addToCartItemList[index].SelectedQty = addToCartItemList[index].SelectedQty + Int32.Parse(qty);
                }
                else //new item clicked
                {
                    addToCartItem = allItem.Where(x => x.ItemID.Equals(itemId)).FirstOrDefault();
                    addToCartItem.SelectedQty = Int32.Parse(qty);
                    addToCartItemList.Add(addToCartItem);
                }
                
                string addedItemJson = Newtonsoft.Json.JsonConvert.SerializeObject(addToCartItemList);
                HttpContext.Session.SetString("addedItemSession", addedItemJson);


            }
            
            return RedirectToAction("Index");
        }
        
        //Basket Partial View
        public ActionResult PartialViewCart()
        {
            return PartialView("_CartPartial");
        }

        //View Cart Page 
        public ActionResult ViewCart()
        {
            string cartItemJson = HttpContext.Session.GetString("addedItemSession");
            List<AddToCartItem> addedItems = new List<AddToCartItem>();
            if (!String.IsNullOrEmpty(cartItemJson))
            {
                addedItems = JsonConvert.DeserializeObject<List<AddToCartItem>>(cartItemJson);
            }
            ViewData["addedItems"] = addedItems;
            return View();
        }

        //public ActionResult UpdateCart(List<AddToCartItem> itemlist)
        //{
        //    string cartItemJson = HttpContext.Session.GetString("addedItemSession");
        //    List<AddToCartItem> addedItems = JsonConvert.DeserializeObject<List<AddToCartItem>>(cartItemJson);

        //    for(int i = 0; i < addedItems.Count; i++)
        //    {
        //        if(addedItems[i].ItemID == itemId)
        //        {
        //            if()
        //            addedItems[i].SelectedQty = Int32.Parse(qty);
        //        }
        //    } 
        //}
    }
}
