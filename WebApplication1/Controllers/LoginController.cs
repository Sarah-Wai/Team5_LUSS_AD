using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace LUSS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        /*
        [HttpGet("{Email}/{Password}")]
        [Route("CheckLogin/{Email}/{Password}")]
        public User CheckLogin(string Email,string Password)
        {
            User user = (from i in context123.User
                         where i.Email == Email && i.Password== Password
                         select i).FirstOrDefault();
            return user;
        }
        */

        [HttpGet("{Email}/{Password}")]
        [Route("CheckLogin/{Email}/{Password}")]
        public User CheckLogin(string Email, string Password)
        {
            User user = (from i in context123.User
                         where i.Email == Email && i.Password == Password
                         select i).FirstOrDefault();
            User retun_user = new User()
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ContactNumber = user.ContactNumber,
                DepartmentID=user.DepartmentID,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                IsRepresentative = user.IsRepresentative,
                Designation = user.Designation,
                ReportToID = user.ReportToID,
                Department= user.Department == null? context123.Department.First(x => x.DepartmentID == user.DepartmentID): user.Department ,
        };

         
            return retun_user;
        }

        [HttpGet("MobileLogin/{Email}/{Password}")]
        public User MCheckLogin(string Email, string Password)
        {
            string hpwd = Encrypt(Password);
            User user = CheckLogin(Email, hpwd);
            User n_user = new User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DepartmentID = user.DepartmentID,
                Role = user.Role,
                UserID = user.UserID,
            };
            return n_user;

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
