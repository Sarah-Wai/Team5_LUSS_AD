using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        public MyDbContext context123;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet("get-representative/{id}")]
        //public User GetDeptRep(int id)
        //{
        //    User rep = context123.User.First(x => x.DepartmentID == id && x.IsRepresentative == true);
        //    return rep;
        //}

        public User GetDeptRep(int id)
        {
            User rep = context123.User.Where(x => x.DepartmentID == id && x.IsRepresentative == true).Select(c =>

            new User
            {
                UserID = c.UserID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Department = new Department
                {
                    DepartmentCode = c.Department.DepartmentCode,
                    DepartmentName = c.Department.DepartmentName,
                    CollectionPoint = new CollectionPoint { Location = c.Department.CollectionPoint.Location, Description = c.Department.CollectionPoint.Description }
                }

            }).FirstOrDefault(); ;
            return rep;
        }

        [HttpGet("get-rept-lower/{id}")]
        public User GetDepRep_lower(int id)
        {
            User rep = context123.User.Where(x => x.DepartmentID == id && x.IsRepresentative == true).Select(c => 
            
            new User
            {
                UserID = c.UserID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Department = new Department { DepartmentCode = c.Department.DepartmentCode, DepartmentName = c.Department.DepartmentName,
                    CollectionPoint = new CollectionPoint { Location = c.Department.CollectionPoint.Location, Description = c.Department.CollectionPoint.Description} }

            }).FirstOrDefault();
            return rep;
        }


        [HttpGet("{id}")]
        [Route("GetAllDeptUsers/{id}")]
        public IEnumerable<User> GetAllDeptUsers(int id)
        {
            List<User> Users = context123.User
                  .Where(x => x.DepartmentID == id).ToList();
            return Users;

        }
        [HttpGet("{id}")]
        [Route("GetAllDeptEmpUsers/{id}")]
        public IEnumerable<User> GetAllDeptEmpUsers(int id)
        {
            List<User> Users = context123.User
                  .Where(x => x.DepartmentID == id && x.ReportToID!=null).ToList();
            return Users;

        }

        [HttpGet("{id}")]
        [Route("GetAllRepresentativeUsers/{id}")]
        public IEnumerable<User> GetAllRepresentativeUsers(int id)
        {
            List<User> Users = context123.User
                  .Where(x => x.DepartmentID == id && x.ReportToID != null && x.Role!= "dept_delegate").ToList();
            return Users;

        }


        [HttpGet("{id}")]
        [Route("GetUsersByID/{id}")]
        public User GetUsersByID(int id)
        {
            User user = context123.User
                  .First(x => x.UserID == id);
            return user;

        }

        [HttpGet("{id}")]
        [Route("GetAllDeptUsersMB/{id}")]
        public IEnumerable<User> GetAllDeptUsersMB(int id)
        {
            List<User> Users = context123.User.Where(x => x.DepartmentID == id).ToList();
            List<User> return_users = new List<User>();
            foreach (User u in Users)
            {
                User newUser = new User()
                {
                   UserID = u.UserID, 
                   FirstName = u.FirstName,
                   LastName = u.LastName 
                };
                return_users.Add(newUser);
            }
            return return_users;
        }

    }
}
