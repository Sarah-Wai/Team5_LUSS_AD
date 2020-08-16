using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using static LUSS_API.Models.Status;


namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {

        public MyDbContext context123;
        private readonly ILogger<CollectionPointController> _logger;
        public RequestController(ILogger<CollectionPointController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }


        [HttpGet]
        [Route("getAllRequest")]
        public IEnumerable<Request> GetAllRequest()
        {
            List<Request> requests = context123.Request.ToList();
            return requests;

        }


        [HttpGet("{id}/{comment}")]
        [Route("ApproveRequestByDepHead/{id}/{comment}")]
        public Request ApproveRequestByDepHead(int id,string comment)
        {

            Request getRequest = context123.Request
                  .Where(x => x.RequestID == id).SingleOrDefault();
            if (getRequest != null)
            {
                getRequest.Comment = comment;
                getRequest.RequestStatus = EOrderStatus.Approved;
                context123.SaveChanges();
            }

            return getRequest;
        }

        [HttpGet]
        public IEnumerable<Request> Get()
        {
            List<Request> requestList = context123.Request.Where(x => x.RequestStatus != EOrderStatus.Rejected && x.RequestStatus != EOrderStatus.Pending).ToList();
            return requestList;
        }


        [HttpGet("{status}")]
        public IEnumerable<dynamic> GetRequestByStatus(string status)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
            List<Request> requests = context123.Request.ToList();
            List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
            List<Item> items = context123.Item.ToList();

            var iter = (from r in requests
                       join rd in requestDetailsList on r.RequestID equals rd.RequestID 
                       where r.RequestStatus.Equals(st)
                       group rd by rd.ItemID into n
                       join i in items on n.FirstOrDefault().ItemID equals i.ItemID
                       select new {
                           ItemCode = i.ItemCode,
                           TotalQty = n.Sum(x => x.RequestQty),
                           ItemName = i.ItemName,
                           ItemUOM = i.UOM,
                           CollectionTime = n.Select(x=>x.Request.CollectionTime).First(),
                           RequestIds = n.Select(x=>x.Request.RequestID).ToList()
                       }).ToList();
            return iter;
        }

/*        [HttpGet("{id}")]
        public Request GetById(int id)
        {
            Request request = context123.Request.Where(x => x.RequestID == id).First();
            return request;
        }*/
    }
      
    }

      

