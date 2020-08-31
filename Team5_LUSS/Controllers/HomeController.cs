using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Team5_LUSS.Models;

namespace Team5_LUSS.Controllers
{
    public class HomeController : Controller
    {

        string api_url = "https://localhost:44312/Login"; 
        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyTeamIsTeam5SuperHeroes"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://www.yogihosting.com",
                audience: "https://www.yogihosting.com",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var tokenString = GenerateJSONWebToken();
            User login_user = new User(); 
            string Hword = Encrypt(Password); 
            string action_name = ""; string controller_name = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.GetAsync(api_url + "/CheckLogin/" + Email + "/" + Hword)) 
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse != "" && !apiResponse.Contains("<!DOCTYPE html"))
                    {
                        login_user = JsonConvert.DeserializeObject<User>(apiResponse);
                    }
                }

                if (login_user.UserID != 0)
                {
                    ViewData["User"] = login_user;
                    HttpContext.Session.SetString("Email", Email);
                    HttpContext.Session.SetString("User", login_user.FirstName);
                    HttpContext.Session.SetInt32("UserID", login_user.UserID);
                    HttpContext.Session.SetInt32("CPId", login_user.Department.CollectionPointID);
                    HttpContext.Session.SetInt32("DeptId", login_user.DepartmentID);
                    HttpContext.Session.SetString("UserRole", login_user.Role);

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "UserObj", login_user);
                    TempData["Alert"] = "Login Successful";

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

            TempData["Alert"] = "Login Failed";
            return View("Index");
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
