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

       
        public static async void Email(int UserID, string subject)
        {
            //use UserID -> get receipiantID, -> get Email 
            //if subject = reqt approve, 
            User receiver = new User();
            MailMessage mm = new MailMessage();
            User user = new User();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/get_manager/" + UserID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receiver = JsonConvert.DeserializeObject<User>(apiResponse);
                }

                using (var response = await httpClient.GetAsync(getURL() + "/get_manager/" + UserID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }

            //switch case
            //case: new request  subject ="XXX wait for approve"; body ="xxsdfsfs"


            switch (subject)
            {
             
                case "newVoucherToDeptHead":
                  mm.To.Add(receiver.Email); // content specific
                    string name = receiver.FirstName;
                    mm.Subject = "Voucher Approved"; // content specific
                    mm.Body =   " Dear" + name +  "There is a new adjustment voucher waiting for your approval." +
                        "Please login to LU Stationery System to perform further actions."; // content specific
                    mm.From = new MailAddress("team5luss@gmail.com");
                    mm.IsBodyHtml = false;
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;
                    client.UseDefaultCredentials = true;
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential
                        ("team5luss@gmail.com", "Profesther");
                    client.Send(mm);
                    break;


                case "newRequest":
                    mm.To.Add(receiver.Email); // content specific
                    name = receiver.FirstName;
                    mm.Subject = "Voucher Approved"; // content specific
                    mm.Body = " Dear" + name + ", There is a new request waiting for your approval." +
                        "Please login to LU Stationery System to perform further actions.";
                    mm.IsBodyHtml = false;
                    client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;
                    client.UseDefaultCredentials = true;
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential
                        ("team5luss@gmail.com", "Profesther");
                    client.Send(mm);
                    break;

                case "ApproveRequest":
                    mm.To.Add(receiver.Email); // content specific
                    name = receiver.FirstName;
                    mm.Subject = "Request Approved"; // content specific
                    mm.Body = " Dear" + name + ", There is a new request waiting for your approval." +
                        "Please login to LU Stationery System to perform further actions.";
                    mm.IsBodyHtml = false;
                    client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;
                    client.UseDefaultCredentials = true;
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential
                        ("team5luss@gmail.com", "Profesther");
                    client.Send(mm);
                    break;


            }
        }
    }
}
// Delegate email
// MailMessage mm = new MailMessage(); // (email address >> receiver, subject, body )
//                    mm.To.Add(receiver.Email); // content specific
//                    name = receiver.FirstName;
//                    mm.Subject = "Assigned delagate"; // content specific
//                    mm.Body = " Dear" + name + ", You have been assigned as delegate" +
//                       
//                    mm.IsBodyHtml = false;
//                    SmtpClient client = new SmtpClient("smtp.gmail.com");
//                    client.Port = 587;
//                    client.UseDefaultCredentials = true;
//                    client.EnableSsl = true;
//                    client.Credentials = new System.Net.NetworkCredential
//                        ("team5luss@gmail.com", "Profesther");
//                    client.Send(mm);
//                    break;




//Request Approved

//MailMessage mm = new MailMessage(); // (email address >> receiver, subject, body )
//            {
//                name = receiver.FirstName;
//                mm.To.Add(user.Email); // content specific
//                mm.Subject = "Request Approved"; // content specific
//                mm.Body = RequestID + " has been Approved"; // content specific
//                mm.From = new MailAddress("team5luss@gmail.com");
//                mm.IsBodyHtml = false;
//                SmtpClient client = new SmtpClient("smtp.gmail.com");
//                client.Port = 587;
//                client.UseDefaultCredentials = true;
//                client.EnableSsl = true;
//                client.Credentials = new System.Net.NetworkCredential
//                    ("team5luss@gmail.com", "Profesther");
//                client.Send(mm);
//                ViewBag.message = "Email notification sent";
//            }



//Request Rejected
//MailMessage mm = new MailMessage(); // (email address >> receiver, subject, body )
//            {
//                name = receiver.FirstName;
//                mm.To.Add(user.Email); // content specific
//                mm.Subject = "Request Rejected"; // content specific
//                mm.Body = RequestID + " has been Rejected"; // content specific
//                mm.From = new MailAddress("team5luss@gmail.com");
//                mm.IsBodyHtml = false;
//                SmtpClient client = new SmtpClient("smtp.gmail.com");
//                client.Port = 587;
//                client.UseDefaultCredentials = true;
//                client.EnableSsl = true;
//                client.Credentials = new System.Net.NetworkCredential
//                    ("team5luss@gmail.com", "Profesther");
//                client.Send(mm);
//                ViewBag.message = "Email notification sent";
//            }



//Order Completed
//MailMessage mm = new MailMessage(); // (email address >> receiver, subject, body )
//            {
//                name = receiver.FirstName;
//                mm.To.Add(user.Email); // content specific
//                mm.Subject = "Order Completed"; // content specific
//                mm.Body ="Dear " + user.Firstname + ","+ OrderID + " has been Completed"; // content specific
//                mm.From = new MailAddress("team5luss@gmail.com");
//                mm.IsBodyHtml = false;
//                SmtpClient client = new SmtpClient("smtp.gmail.com");
//                client.Port = 587;
//                client.UseDefaultCredentials = true;
//                client.EnableSsl = true;
//                client.Credentials = new System.Net.NetworkCredential
//                    ("team5luss@gmail.com", "Profesther");
//                client.Send(mm);
//                ViewBag.message = "Email notification sent";
//            }
