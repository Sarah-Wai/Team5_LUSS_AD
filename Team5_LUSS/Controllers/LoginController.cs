using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Controllers
{
    public class LoginController
    {
        public LoginController(MyDbContext dbcontext) // connecting to DB
        {
            this.dbcontext = dbcontext;
        }
        private readonly DbContext db;
        public DbSet<Customer> Customers { get; set; }
        public object Session { get; private set; }

        [HttpPost]
        public IActionResult Add(string Email, string Password) // Authenticating User
        {
            ViewData["Email"] = HttpContext.Session.GetString("Email");
            ViewData["welcome"] = HttpContext.Session.GetString("welcome");

            string Hword = Encrypt(Password);
            var userDetails = dbcontext.Customers.Where(x => x.Email == Email && x.Password == Hword).FirstOrDefault();


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
                HttpContext.Session.SetInt32("UserID", userDetails.Id);
                TempData["Alert"] = "Login Successful";

                if (HttpContext.Session.GetString("cartHistoryList") != null && HttpContext.Session.GetString("cartHistoryList") != "")
                {
                    return RedirectToAction("AppendUserIDToSessionCart", "MyCart");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }


        }
    }
}
