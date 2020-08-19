using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Team5_LUSS.Controllers
{
    public class DHeadDashController : Controller
    {
       
        public IActionResult Index()
        {
            Console.WriteLine("test");
            return View("DHeadDash");
        }
      

    }
}