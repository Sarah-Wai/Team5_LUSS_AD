﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace LUSS_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class RepresentativeController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<RepresentativeController> _logger;
        public RepresentativeController(ILogger<RepresentativeController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }


        [HttpGet("{id}/{status}")]
        [Route("SaveRepresentative/{id}/{status}")]
        public string SaveRepresentative(int id,bool status)
        {
            User representativeUser = context123.User.First(c => c.UserID == id);
            
            if (representativeUser != null)
            {
               
                representativeUser.IsRepresentative = status;
                representativeUser.DelegatedManager = null;
                representativeUser.RequestedBy = null;
                representativeUser.RequestMade = null;
                representativeUser.RequestModified = null;
                representativeUser.RequestModified = null;

            }
            
            try
            {
               context123.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return "Success";
        }

          

    }
}