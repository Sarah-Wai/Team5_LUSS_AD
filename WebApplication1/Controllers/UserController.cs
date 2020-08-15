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

        [HttpGet("{deptId}")]
        public User GetDeptRep(int deptId)
        {
            User rep = context123.User.First(x => x.DepartmentID == deptId && x.IsRepresentative == true);
            return rep;
        }

    }
}
