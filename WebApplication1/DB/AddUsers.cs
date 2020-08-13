using System;
using System.Collections.Generic;
using System.Linq;
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
                UserID=1,
                FirstName="Wai" ,
                LastName="Phu",
                ContactNumber="97744121",
                Email="wai@gmail.com",
                Password="123",
                Role= "dept_head",
                IsRepresentative=false,
                ReportToID=null,
                DepartmentID=1
            };
            User u2 = new User()
            {
                UserID=2,
                FirstName = "Sarah",
                LastName = "Su",
                ContactNumber = "97744121",
                Email = "sarah@gmail.com",
                Password = "123",
                Role = "dept_employee",
                IsRepresentative = false,
                ReportToID = u1.UserID,
                DepartmentID = 1
            };
            //User u3 = new User()
            //{

            //};
            //User u4 = new User()
            //{

            //};
            users.Add(u1);
            users.Add(u2);
            return users;
        }
    }
}
