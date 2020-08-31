using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Team5_LUSS.Models;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Team5_LUSS.Controllers
{
    public class DelegateController : Controller
    {
        string api_user_url = "https://localhost:44312/User/GetAllDeptEmpUsers";
        string api_delegate_url = "https://localhost:44312/Delegate";
        string msg = "";
        public IActionResult Delegate()
        {
            ViewData["DeleteMsg"] = TempData["Msg"];
            return View();
        }

        public IActionResult DeleteDelegate(int id)
        {
            Task<string> tesk = DeleteDelegateTask(id);
            return RedirectToAction("AddDelegate");
        }

        [HttpGet]
        public async Task<IActionResult> AddDelegate(string message)
        {
            List<User> users = new List<User>();
            int id = (int)HttpContext.Session.GetInt32("DeptId");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_user_url + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            if (message != "" && message!="null" && message!=null)
            {
                ViewData["DeleteMsg"] = message;
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

        public async Task<bool> isActiveDelegateByDateNDepID(DelegatedManager delegatedManager)
        {
            int deptID = (int)HttpContext.Session.GetInt32("DeptId");
            bool isActive = false;
            string fromDate = delegatedManager.FromDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            string toDate = delegatedManager.ToDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            using (var httpClient = new HttpClient())
            {
              
                using (var response = await httpClient.GetAsync(api_delegate_url + "/isActiveDelegateByDateNDepID/" + deptID + "/" + fromDate + "/" + toDate))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse != "")
                        isActive = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }
            return isActive;

        }

        [HttpPost]
        public async Task<IActionResult> AddDelegate(DelegatedManager delegatedManager)
        {


            int deptID = (int)HttpContext.Session.GetInt32("DeptId");
            bool isActive = false;
            string fromDate = delegatedManager.FromDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            string toDate = delegatedManager.ToDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync(api_delegate_url + "/isActiveDelegateByDateNDepID/" + deptID + "/" + fromDate + "/" + toDate))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse != "")
                        isActive = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }

            if (!isActive)
            {
                using (var httpClient = new HttpClient())
                {
                    #region save 
                    StringContent content = new StringContent(JsonConvert.SerializeObject(delegatedManager), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(api_delegate_url + "/SaveDelegatedManager", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                    }
                    #endregion

                }
                msg = "Assigned Successfully!";
            }
            else
            {
                msg = "Cannot Assign for this date!";
            }

            return RedirectToAction("AddDelegate",new { message =msg});
        }

    }
}