using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Team5_LUSS.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            Console.WriteLine("test");
            return View();
        }
      
    }
}