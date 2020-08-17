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
        string api_delegate_url = "https://localhost:44312/Delegate";
        string msg = "";
        public IActionResult Delegate()
        {
            ViewData["DeleteMsg"] = TempData["Msg"];
            return View();
        }

        public IActionResult DeleteDelegate(int id)
        {
            Task<string>  tesk=DeleteDelegateTask(id);
            return RedirectToAction("AddDelegate");
        }

        [HttpGet]
        public async Task<IActionResult> AddDelegate(int id)
        {
            List<User> users = new List<User>();
            id = 1;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_user_url + "/"+id ))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            if (msg != "") { 
                ViewData["DeleteMsg"] = msg; 
            }
        
            ViewData["users"] = users;
            return View();
        }
        
       [HttpPost]
        public async Task<DelegatedManager> getDelegateByUserID(int id)
        {
            DelegatedManager delegatedManager = new DelegatedManager();
           
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_delegate_url + "/getDelegateByUserID/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    delegatedManager = JsonConvert.DeserializeObject<DelegatedManager>(apiResponse);
                }
            }
           
            return delegatedManager;
        }

        [HttpPost]
        public async Task<string> DeleteDelegateTask(int id)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(api_delegate_url + "/DeleteDelegate/" + id))
                {
                     apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            msg = "Removed Successfully!";
            return apiResponse;
        }
     

        [HttpPost]
        public async Task<IActionResult> AddDelegate(DelegatedManager delegatedManage)
        {
            string receivedDelegatedManager = "";
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(delegatedManage), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(api_delegate_url+"/SaveDelegatedManager", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                  
                }
            }
            ViewData["products"] = receivedDelegatedManager;
            msg = "Assigned Successfully!";
            return RedirectToAction("AddDelegate");
        }

    }
}
