﻿using System;
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
    public class RequestDetailsController : Controller
    {
        public MyDbContext context123;
        private readonly ILogger<RequestDetailsController> _logger;
        public RequestDetailsController(ILogger<RequestDetailsController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet("{reqid}")]
        public List<RequestDetails> GetRequestDetailsByRequest(int reqid)
        {
            List<RequestDetails> requestItems = context123.RequestDetails.Where(x => x.RequestID == reqid).ToList(); 
            return requestItems;
        }

     
    }
}
