﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Team5_LUSS.Controllers
{
    public class RepresentativeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
