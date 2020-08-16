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


        string api_url = "https://localhost:44312/ItemCategory"; // connect to API project Controller class
        public async Task<IActionResult> ItemCategory()
        {
            List<User> Users = new List<User>(); // create a new list with objects of "ItemCatergory"
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url)) // connect to call api
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Users = JsonConvert.DeserializeObject<List<User>>(apiResponse); // convert the packets from https link to the object
                }
            }
            
            ViewData["Users"] = Users;
            return View();

        }


        private readonly ILogger<HomeController> _logger;

        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login(string Email, string Password)
        {
            string Hword = Encrypt(Password);

            var userDetails = Users.Where(x => x.Email == Email && x.Password == Hword).FirstOrDefault();
            if (userDetails == null)
            {
                //userModel.LoginErrorMessage = "Wrong Credentials.";
                TempData["Alert"] = "Login Failed";
                return RedirectToAction("Index", "Home");
            }

             else
            {
                //Session["userID"] = userDetails.Email;
                HttpContext.Session.SetString("Email", Email);
                HttpContext.Session.SetInt32("UserID", userDetails.UserID);
                TempData["Alert"] = "Login Successful";

                
                //if (HttpContext.Session.GetString("cartHistoryList") != null && HttpContext.Session.GetString("cartHistoryList") != "")
                //if (userDetails.Role == "sales")
                if (userDetails.Department.DepartmentName == "sales")
                {
                    return RedirectToAction("Sales", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

        }


        public IActionResult Index()
        {
           // return View();
           return RedirectToAction("Dashboard", "Dashboard");
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
