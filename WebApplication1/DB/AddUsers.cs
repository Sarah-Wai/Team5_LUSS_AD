using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LUSS_API.Models;

namespace LUSS_API.DB
{
    public class AddUsers
    {
        public static List<User> getAllUsers()
        {
            List<User> users = new List<User>();
            User u1 = new User()
            {
                //UserID=1,
                FirstName = "Wai" ,
                LastName = "Phu",
                ContactNumber = "97744121",
                Email = "wai@gmail.com",
                Password = Encrypt("123"),
                Role = "dept_head",
                IsRepresentative = false,
                Designation = "Department Head",
                ReportToID = null,
                DepartmentID = 2
            };
            User u2 = new User()
            {
                //UserID=2,
                FirstName = "Sarah",
                LastName = "Su",
                ContactNumber = "97744121",
                Email = "sarah@gmail.com",
                Password = Encrypt("123"),
                Role = "dept_delegate",
                Designation = "dept_employee",
                IsRepresentative = false,
                ReportToID = u1.UserID,
                DepartmentID = 2
            };
            User u3 = new User()
            {
                //UserID = 2,
                FirstName = "Nang",
                LastName = "Sandar",
                ContactNumber = "97744121",
                Email = "nang@gmail.com",
                Password = Encrypt("123"),
                Role = "dept_rep",
                Designation = "Assistant",
                IsRepresentative = true,
                ReportToID = u1.UserID,
                DepartmentID = 2
            };
            User u4 = new User()
            {
                //UserID = 2,
                FirstName = "Ian",
                LastName = "Tan",
                ContactNumber = "97744121",
                Email = "iantanze@gmail.com",
                Password = Encrypt("123"),
                Role = "dept_head",
                Designation = "Junior Officer",
                IsRepresentative = false,
                ReportToID = null,
                DepartmentID = 3
            };
            User u5 = new User()
            {
                //UserID = 2,
                FirstName = "Lyra",
                LastName = "Li",
                ContactNumber = "97744121",
                Email = "lyra@gmail.com",
                Password = Encrypt("123"),
                Role = "dept_rep",
                Designation = "Junior Officer",
                IsRepresentative = true,
                ReportToID = u4.UserID,
                DepartmentID = 3
            };
            User u6 = new User()
            {
               // UserID = 2,
                FirstName = "Justin",
                LastName = "Jame",
                ContactNumber = "97744121",
                Email = "justin@gmail.com",
                Password = Encrypt("123"),
                Role = "dept_employee",
                Designation = "Junior Officer",
                IsRepresentative = false,
                ReportToID = u4.UserID,
                DepartmentID = 3
            };

            User u7 = new User()
            {
                // UserID = 8,
                FirstName = "Sherren",
                LastName = "Sosento",
                ContactNumber = "97744121",
                Email = "sherren@gmail.com",
                Password = Encrypt("123"),
                Role = "store_manager",
                Designation = "Senior Officer",
                IsRepresentative = false,
                ReportToID = null,
                DepartmentID = 10
            };
            User u8 = new User()
            {
               // UserID = 8,
                FirstName = "Selly",
                LastName = "Sosento",
                ContactNumber = "97744121",
                Email = "selly@gmail.com",
                Password = Encrypt("123"),
                Role = "store_supervisor",
                Designation = "Junior Officer",
                IsRepresentative = false,
                ReportToID = u7.UserID,
                DepartmentID = 10
            };
           

            User u9 = new User()
            {
                
                FirstName = "Wee",
                LastName = "Kiat",
                ContactNumber = "97744121",
                Email = "weekiat@gmail.com",
                Password = Encrypt("123"),
                Role = "store_clerk",
                Designation = "Assistant",
                IsRepresentative = false,
                ReportToID = u7.UserID,
                DepartmentID = 10
            };
            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);
            users.Add(u7);
            users.Add(u8);
            users.Add(u9);
            return users;

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
}
