using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Team5_LUSS.Models;

namespace Team5_LUSS.Controllers
{
    public class AdjustmentListController : Controller
    {
        string api_url = "https://localhost:44312/AdjustmentList"; // calling the right api
        string api_url1 = "https://localhost:44312/AdjustmentVoucher";



        public async Task<IActionResult> AdjustmentVouchers()
        {
            List<AdjustmentVoucher> adjustments = new List<AdjustmentVoucher>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url1)) // call the api 
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    adjustments = JsonConvert.DeserializeObject<List<AdjustmentVoucher>>(apiResponse);
                }
            }
            ViewData["allAdjustments"] = adjustments;
            return View("AdjustmentList");
        }


        [HttpGet]
        public async Task<IActionResult> Approved(int AdjustmentID)
        {
            string apiResponse = "";
            int userId = (int)HttpContext.Session.GetInt32("UserID");
            User user = new User();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + AdjustmentID + "/" + "Approve" + "/" + "ItemID" + "/" + "AdjustQty" +"/" + "AdjustType"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            

            using (var response = await httpClient.GetAsync(api_url + "/get-manager" + "/" + userId))

                {
                apiResponse = await response.Content.ReadAsStringAsync();
                 user = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            string msg = "Assign Successfully!";

            /*
            MailMessage mm = new MailMessage();
            {
                mm.To.Add(user.Email); // content specific
                mm.Subject = "Voucher Approved"; // content specific
                mm.Body = AdjustmentID + " has been approved"; // content specific
                mm.From = new MailAddress("team5luss@gmail.com");
                mm.IsBodyHtml = false;
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.UseDefaultCredentials = true;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential
                    ("team5luss@gmail.com", "Profesther");
                client.Send(mm);
                ViewBag.message = "Email notification sent";
            }*/

            return RedirectToAction("AdjustmentVouchers", "AdjustmentList"); 
        }

        [HttpGet]
        public async Task<IActionResult> Rejected(int AdjustmentID)
        {
            
            string apiResponse = "";
            int userId = (int)HttpContext.Session.GetInt32("UserID");
            User user = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + AdjustmentID + "/" + "Reject" + "/" + "ItemID" + "/" + "AdjustQty" + "/" + "AdjustType"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
                using (var response = await httpClient.GetAsync(api_url + "/get-manager" + "/" + userId))

                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            string msg = "Remove Successfully!";

            MailMessage mm = new MailMessage(); // (email address >> receiver, subject, body )
            {
                mm.To.Add(user.Email); // content specific
                mm.Subject = "Voucher Rejected"; // content specific
                mm.Body = AdjustmentID + " has been rejected"; // content specific
                mm.From = new MailAddress("team5luss@gmail.com");
                mm.IsBodyHtml = false;
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.UseDefaultCredentials = true;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential
                    ("team5luss@gmail.com", "Profesther");
                client.Send(mm);
                ViewBag.message = "Email notification sent";
            }
            return RedirectToAction("AssignRepresentative", new { id = 1, msg = msg });
        }


    
   // [HttpPost]
        public async Task<IActionResult> VoucherApproveDetails(int AdjustmentID)
        {
            AdjustmentVoucher adjustmentVoucher = new AdjustmentVoucher();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url +"/"+ AdjustmentID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    adjustmentVoucher = JsonConvert.DeserializeObject<AdjustmentVoucher>(apiResponse);
                }
            }
            return View(adjustmentVoucher);
        }
    }
}
