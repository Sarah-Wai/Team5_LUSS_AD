using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


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
            if (user != null)
            {
                if (user.DelegatedManager == null) { user.DelegatedManager = new DelegatedManager(); }
            }
            return user;
        }

    }
}
