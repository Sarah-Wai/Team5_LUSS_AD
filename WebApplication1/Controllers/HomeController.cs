using LUSS_API.DB;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController
    {
        public MyDbContext context123;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }


        [HttpGet]
        public IEnumerable<User> GetUserList()
        {
            List<User> Users = context123.User.ToList();
            return Users;

        }

    }
}
