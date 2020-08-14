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


        [HttpGet("{deptID}/{cpID}")]
        public Department GetDepartment(int deptID, int cpID)
        {
            int dummyID = 1;//TO replace by real ID
            Department dp = context123.Department.Where(x => x.DepartmentID == dummyID).FirstOrDefault();
            dp.CollectionPointID = cpID;
            context123.SaveChanges();

            return dp;
        }

        [HttpPost("{deptID}")]
        public Department UpdateCollectionPoint(int deptID, int cpID)
        {
            int dummyID = 1;//TO replace by real ID
            Department dp = context123.Department.Where(x => x.DepartmentID == dummyID).FirstOrDefault();
            dp.CollectionPointID = cpID;
            context123.SaveChanges();

            return dp;
        }

    }
}
