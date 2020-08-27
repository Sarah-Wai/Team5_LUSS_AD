using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Team5_LUSS.Models;

namespace Team5_LUSS.Controllers
{
    public class HomeController : Controller
    {

        List<User> Users = new List<User>();


        string api_url = "https://localhost:44312/Login"; // connect to API project Controller class
        public async Task<IActionResult> Login(string Email, string Password)
        {
            User  login_user = new User(); // create a new objects of "User"
            string Hword = Encrypt(Password);    // string Hword = Encrypt(Password);    Change Later
            string action_name = "";string controller_name = "";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url+"/CheckLogin/" + Email+"/"+ Hword)) // connect to call api
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    login_user = JsonConvert.DeserializeObject<User>(apiResponse); // convert the packets from https link to the object
                }
                if (login_user == null)
                {
                    //userModel.LoginErrorMessage = "Wrong Credentials.";
                    TempData["Alert"] = "Login Failed";
                    return RedirectToAction("Privacy", "Home");
                }

                else
                {
                    ViewData["User"] = login_user;
                    HttpContext.Session.SetString("Email", Email);
                    HttpContext.Session.SetString("User", login_user.FirstName);
                    HttpContext.Session.SetInt32("UserID", login_user.UserID);
                    HttpContext.Session.SetInt32("CPId", login_user.Department.CollectionPointID);
                    HttpContext.Session.SetInt32("DeptId", login_user.DepartmentID);
                    HttpContext.Session.SetString("UserRole", login_user.Role);

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "UserObj", login_user);


                   // List<WtrStudent> _sessionList = SessionHelper.GetObjectFromJson<List<WtrStudent>>(HttpContext.Session, "userObject");
                    TempData["Alert"] = "Login Successful";


                    //if (HttpContext.Session.GetString("cartHistoryList") != null && HttpContext.Session.GetString("cartHistoryList") != "")
                    //if (userDetails.Role == "sales")
                    //store_clerk / store_supervisor / store_manager /  dept_employee / dept_rep / dept_delegate / dept_head
                    //if (login_user.Department.DepartmentName == "store")
                    //{
                    //    switch (login_user.Role)
                    //    {
                    //        case "store_clerk": action_name = "Index"; controller_name = "ItemList"; break;
                    //        case "store_supervisor": action_name = "Dashboard"; controller_name = "Dashboard"; break;
                    //        case "store_manager": action_name = "Dashboard"; controller_name = "Dashboard"; break;  ;
                    //    }
                    //    return RedirectToAction("Dashboard", "Dashboard");
                    //}
                    //else
                    //{
                    //    switch (login_user.Role)
                    //    {
                    //        case "dept_employee": action_name = "Index";controller_name = "ItemList"; break;
                    //        case "dept_head": action_name = "Index"; controller_name = "DHeadDash"; break;
                    //            //case "dept_delegate": break;
                    //    }

                    //}
                    //return RedirectToAction(action_name, controller_name);

                    switch (login_user.Role)
                    {
                        case "store_clerk": action_name = "Index"; controller_name = "ClerkDash"; break;
                        case "store_supervisor": action_name = "Index"; controller_name = "SupDash"; break;
                        case "store_manager": action_name = "Index"; controller_name = "SupDash"; break;
                        case "dept_rep": action_name = "Index"; controller_name = "ItemList"; break; 
                        case "dept_delegate": action_name = "StationeryRequests"; controller_name = "StationeryRequests"; break; 
                        case "dept_head": action_name = "Index"; controller_name = "DHeadDash"; break;
                        case "dept_employee": action_name = "Index"; controller_name = "ItemList"; break;
                    }
                    return RedirectToAction(action_name, controller_name);
                }
                }
        }


        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
           
        }
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            TempData["Alert"] = "Successfully Logout!";
            Console.WriteLine("Logout complete");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        static string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }
    }
}
