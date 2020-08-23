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
                //UserID=1,
                FirstName="Wai" ,
                LastName="Phu",
                ContactNumber="97744121",
                Email="wai@gmail.com",
                Password="123",
                Role= "dept_head",
                IsRepresentative=false,
                Designation="Department Head",
                ReportToID=2,
                DepartmentID=1
            };
            User u2 = new User()
            {
                //UserID=2,
                FirstName = "Sarah",
                LastName = "Su",
                ContactNumber = "97744121",
                Email = "iantanze@gmail.com",
                Password = "123",
                Role = "dept_employee",
                Designation = "Intern",
                IsRepresentative = true,
                ReportToID = null,
                DepartmentID = 1
            };
            User u3 = new User()
            {
                //UserID = 2,
                FirstName = "Nang",
                LastName = "Sandar",
                ContactNumber = "97744121",
                Email = "sarah@gmail.com",
                Password = "123",
                Role = "dept_employee",
                Designation = "Assistant",
                IsRepresentative = true,
                ReportToID = null,
                DepartmentID = 1
            };
            User u4 = new User()
            {
                //UserID = 2,
                FirstName = "Ian",
                LastName = "Tan",
                ContactNumber = "97744121",
                Email = "sarah@gmail.com",
                Password = "123",
                Role = "dept_employee",
                Designation = "Junior Officer",
                IsRepresentative = true,
                ReportToID = u1.UserID,
                DepartmentID = 1
            };
            User u5 = new User()
            {
                //UserID = 2,
                FirstName = "Lyra",
                LastName = "Yti",
                ContactNumber = "97744121",
                Email = "sarah@gmail.com",
                Password = "123",
                Role = "dept_employee",
                Designation = "Junior Officer",
                IsRepresentative = true,
                ReportToID = null,
                DepartmentID = 1
            };
            User u6 = new User()
            {
               // UserID = 2,
                FirstName = "Justin",
                LastName = "Jame",
                ContactNumber = "97744121",
                Email = "sarah@gmail.com",
                Password = "123",
                Role = "dept_employee",
                Designation = "Senior Officer",
                IsRepresentative = true,
                ReportToID = null,
                DepartmentID = 1
            };
            User u7= new User()
            {
                //UserID = 2,
                FirstName = "Wee",
                LastName = "Kiat",
                ContactNumber = "97744121",
                Email = "sarah@gmail.com",
                Password = "123",
                Role = "dept_employee",
                Designation = "Assistant",
                IsRepresentative = true,
                ReportToID = null,
                DepartmentID = 1
            };
            User u8 = new User()
            {
               // UserID = 8,
                FirstName = "Selly",
                LastName = "Sosento",
                ContactNumber = "97744121",
                Email = "sarah@gmail.com",
                Password = "123",
                Role = "dept_employee",
                Designation = "Junior Officer",
                IsRepresentative = true,
                ReportToID = null,
                DepartmentID = 1
            };
            users.Add(u1);
            users.Add(u2);
            users.Add(u3);
            users.Add(u4);
            users.Add(u5);
            users.Add(u6);
            users.Add(u7);
            users.Add(u8);
            return users;
        }
    }
}
