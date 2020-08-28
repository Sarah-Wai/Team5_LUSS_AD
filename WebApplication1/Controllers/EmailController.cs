using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.DB;
using System.Net.Http;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LUSS_API.Models;

namespace LUSS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        public  MyDbContext context123;
        
        public EmailController(MyDbContext context123)
        {
         
            this.context123 = context123;
        }

        [HttpGet("SendingEmail/{toUserID}/{template}")]
        public string SendingEmail(int toUserID,string template)
        {
            string result = "Fail";
            User toUser = context123.User.First(x => x.UserID == toUserID);
            if (toUser != null) {
                result =SendEmail(toUser.Email, toUser.FirstName + " " + toUser.LastName, template);
            }
            return result;
        }
        public static string SendEmail(string toEmail, string toName, string template)
        {
            string email_body = "";
            string email_subject = "";
            MailMessage mm = new MailMessage();
            switch (template)
            {
                case "newVoucherToDeptHead":
                    email_subject = "Voucher Approved"; // content specific
                    email_body = " Dear " + toName + ", There is a new adjustment voucher waiting for your approval." +
                        "Please login to LU Stationery System to perform further actions."; // content specific
                    break;

                case "approvedVoucherToDeptHead":
                    email_subject = "Voucher Approved"; // content specific
                    email_body = "Dear " + toName + ",  Your adjustment voucher has been approved." +
                        "Please login to LU Stationery System to perform check on the latest status."; // content specific
                    break;

                case "rejectedVoucherToDeptHead":
                    email_subject = "Request is Rejected"; // content specific
                    email_body = "Dear "  + toName + ",  Your adjustment voucher has been rejected." +
                         "Please login to LU Stationery System to perform check on the latest status.";//content specific
                    break;

                case "newRequestToDepHead":
                    email_subject = " Request Pending Approval"; // content specific
                    email_body = " Dear " + toName + ", There is a new request waiting for your approval." +
                        "Please login to LU Stationery System to perform further actions.";
                    break;

                case "approveRequestToEmp":
                    email_subject = "Request is Approved"; // content specific
                    email_body = " Dear " + toName + ", There is a new adjustment voucher waiting for your approval." +
                        "Please login to LU Stationery System to perform further actions."; // content specific
                    break;

                case "rejectRequestToEmp":
                    email_subject = "Request Approved"; // content specific
                    email_body = "Dear " + toName + ",  Your request has been Rejected."+
                        "Please login to LU Stationery System to perform check on the latest status."; // content specific
                    break;

                case "DelegateToEmp":
                    email_subject = "Assign Delegate"; // content specific
                    email_body = " Dear " + toName + " ,You have been assign as delegate." +
                        "Please login to LU Stationery System to perform further actions."; // content specific
                    break;

                case "DisbursementDone":
                    email_subject = "Request Approved"; // content specific
                    email_body = "Dear " + toName + ", your request is ready for collection.Please login to LU Stationary System to perform check on the latest status.";
                      // content specific
                    break;

                case "OrderCompleted":
                    email_subject = "Request is Completed"; // content specific
                    email_body = "Dear " + toName + ",  your request has been completed.Please login to LU Stationary System to perform check on the latest status";
                     // content specific
                    break;


                 
                default: break;
            }

            //send Email
            try
            {

                mm.To.Add(toEmail); // content specific
                mm.Subject = email_subject; // content specific
                mm.Body = email_body;
                mm.IsBodyHtml = false;
                mm.From = new MailAddress("team5luss@gmail.com");
                mm.IsBodyHtml = false;
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.UseDefaultCredentials = true;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential
                    ("team5luss@gmail.com", "Profesther");
                client.Send(mm);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Fail";
            
            }


        }
    }
}

