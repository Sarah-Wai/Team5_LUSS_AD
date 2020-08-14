using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class PurchaseOrderItemsController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<CollectionPointController> _logger;
        public PurchaseOrderItemsController(ILogger<CollectionPointController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }
    }
}