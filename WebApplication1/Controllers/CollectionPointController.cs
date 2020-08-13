using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollectionPointController : ControllerBase
    {

        public MyDbContext context123;
        private readonly ILogger<CollectionPointController> _logger;
        public CollectionPointController(ILogger<CollectionPointController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<CollectionPoint> GetAllCollectionPoints()
        {
            List<CollectionPoint> collectionPoints = context123.CollectionPoint.ToList();
            return collectionPoints;
        }

        [HttpGet("{id}")]
        public CollectionPoint GetCollectionPointByDeptID(int id)
        {
            CollectionPoint collectionPoint = (from d in context123.Department
                                               where d.DepartmentID.Equals(id)
                                               select d.CollectionPoint).FirstOrDefault();
            return collectionPoint;
        }

    }
}
