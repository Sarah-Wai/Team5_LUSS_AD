using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Team5_LUSS.Models;
using static Team5_LUSS.Models.AdjustmentVoucherStatus;

namespace Team5_LUSS.Controllers
{
    public class AdjustmentListController : Controller
    {
        string api_url = "https://localhost:44312/AdjustmentList";



        public async Task<IActionResult> AdjustmentVouchers()
        {
            List<AdjustmentVoucher> adjustmentsUp = new List<AdjustmentVoucher>();
            List<AdjustmentVoucher> adjustmentsDown = new List<AdjustmentVoucher>();
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/" + "pendingUp")) 
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    adjustmentsUp = JsonConvert.DeserializeObject<List<AdjustmentVoucher>>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(api_url + "/" + "pendingDown"))  
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    adjustmentsDown = JsonConvert.DeserializeObject<List<AdjustmentVoucher>>(apiResponse);
                }
            }
            
            ViewData["allAdjustmentsUp"] = adjustmentsUp;
            ViewData["allAdjustmentsDown"] = adjustmentsDown;
            return View("AdjustmentList");
        }


        [HttpPost]
        public async Task<IActionResult> Approved(int AdjustmentID, string Comment)
        {
            string apiResponse = "";
            int userId = (int)HttpContext.Session.GetInt32("UserID");
            User user = new User();
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/ApprovedAdjustmentVoucher/" + AdjustmentID + "/" + Comment + "/" + userId + "/Approved"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            string msg = "Assign Successfully!";


            MailMessage mm = new MailMessage();
            {
                string Email = "iantanze@gmail.com";
                mm.To.Add(Email); 
                mm.Subject = "Voucher Approved"; 
                mm.Body = "Adjustment Voucher:" + AdjustmentID + ", has been approved"; 
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
            
            return RedirectToAction("AdjustmentVouchers", "AdjustmentList"); 
        }

        [HttpPost]
        public async Task<IActionResult> Rejected(int AdjustmentID, string Comment)
        {
            
            string apiResponse = "";
            int userId = (int)HttpContext.Session.GetInt32("UserID");
            User user = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(api_url + "/ApprovedAdjustmentVoucher" + "/" + AdjustmentID + "/" + Comment + "/" + userId + "/Rejected"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            string msg = "Remove Successfully!";

            MailMessage mm = new MailMessage();
            {
                string Email = "iantanze@gmail.com";
                mm.To.Add(Email);
                mm.Subject = "Voucher Rejected"; 
                mm.Body = "Adjustment Voucher:" + AdjustmentID + ", has been rejected.";
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

            return RedirectToAction("AdjustmentVouchers", "AdjustmentList");
        }


    
    //[HttpPost]
        public async Task<IActionResult> VoucherDetails(int AdjustmentID)
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
