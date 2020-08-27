using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using static LUSS_API.Models.Status;

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
        public CollectionPoint UpdateCollectionPoint(int deptID, int cpID)
        {
            
            Department dp = context123.Department.Where(x => x.DepartmentID == deptID).FirstOrDefault();
            dp.CollectionPointID = cpID;
            context123.SaveChanges();
            CollectionPoint cp = context123.CollectionPoint.Where(x => x.CollectionPointID == cpID).FirstOrDefault();
            return cp;
        }

        [HttpGet("{cpID}")]
        public CollectionPoint getCollectionPointByID(int cpID)
        {
            CollectionPoint cp = context123.CollectionPoint.Where(x => x.CollectionPointID == cpID).FirstOrDefault();
            return cp;
        }

        [HttpGet("ByDeptID/{deptID}")]
        public string getCollectionPointByDeptID(int deptID)
        {
            Department d = context123.Department.Where(x => x.DepartmentID == deptID).FirstOrDefault();
            string collectionPoint = d.CollectionPoint.Location + " " + d.CollectionPoint.Description;
            return collectionPoint;
        }

        [HttpGet("collectiontimes/{deptId}")]
        public List<Request> getdepCollectionTime(int deptId)
        {
            List<Request> requests = context123.Request.Where(x => x.RequestStatus == EOrderStatus.PendingDelivery && x.RequestByUser.DepartmentID == deptId).ToList();

            List<Request> collectionTimes = (List<Request>)(from r in requests
                                                            select new Request
                                                            {
                                                                CollectionTime = r.CollectionTime,
                                                                RetrievalID = r.RetrievalID
                                                            }).ToList();

            return collectionTimes;
        }
    }
}
