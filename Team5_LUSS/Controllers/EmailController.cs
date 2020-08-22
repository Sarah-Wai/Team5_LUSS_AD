using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Team5_LUSS.Models;

namespace Team5_LUSS.Controllers
{
    public class EmailController : Controller
    {
       


        public static string getURL()
        {
            return "https://localhost:44312/AdjustmentList";
        }


        public static async void Email (int UserID, string subject)
            {
            //use UserID -> get receipiantID, -> get Email 
            //if subject = reqt approve, 
            User receiver = new User();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/get_manager/" + UserID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receiver = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }

            //switch case
            //case: new request  subject ="XXX wait for approve"; body ="xxsdfsfs"

            /*
            MailMessage mm = new MailMessage();
            {
                mm.To.Add(receiver.Email); // content specific
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
            }
            */
        }
    }
}
