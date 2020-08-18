using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClerkDashController : ControllerBase
    {

        public MyDbContext context123;
        private readonly ILogger<ClerkDashController> _logger;
        public ClerkDashController(ILogger<ClerkDashController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }
        

        [HttpGet]
        public string GetName()
        {            
            int dummyID = 1;//TO replace by real ID
            User currentUser = context123.User.Where(x => x.UserID == dummyID).FirstOrDefault();
            
            return currentUser.FirstName.ToString();
        }

        

    }
}
