using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        [HttpGet("{status}")]
        [Route("GetRequestByStatus/{status}")]
        public IEnumerable<Request> GetRequestByStatus(string status)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
            List<Request> requestList = requestList = context123.Request.Where(x => x.RequestStatus == st).ToList();

            return requestList;
        }


        [HttpGet("{status}/{deptId}")]
        [Route("GetRequestByStatusByDept/{status}/{deptId}")]
        public IEnumerable<Request> GetRequestByStatusByDept(string status, int deptId)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
            List<Request> requestList = requestList = context123.Request.Where(x => x.RequestStatus == st && x.RequestByUser.DepartmentID == deptId).ToList();
            return requestList;
        }



        [HttpGet("get-request/{id}")]
        public Request GetById(int id)
        {
            Request request = context123.Request.Where(x => x.RequestID == id).FirstOrDefault();
            return request;
        }


        [HttpGet("{id}/{comment}")]
        [Route("ApproveRequestByDepHead/{id}/{comment}")]
        public Request ApproveRequestByDepHead(int id, string comment)
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
        [Route("get-approved-request")]
        public IEnumerable<Request> Get()
        {
            List<Request> requestList = context123.Request.Where(x => x.RequestStatus == EOrderStatus.Approved).ToList();
            return requestList;
        }

        [HttpGet("{status}/{retrievalID}")]
        [Route("GetItemByStatus/{status}/{retrievalID}")]
        public IEnumerable<dynamic> GetItemsByStatus(string status, int retrievalId)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
            List<Request> requests = context123.Request.Where(x => x.RequestStatus == st).ToList(); //all request with status = st
            List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
            List<Item> items = context123.Item.ToList();

            var iter = (from r in requests
                        where r.RetrievalID == retrievalId
                        join rd in requestDetailsList on r.RequestID equals rd.RequestID
                        group rd by rd.ItemID into n
                        join i in items on n.FirstOrDefault().ItemID equals i.ItemID
                        select new
                        {
                            itemIds = i.ItemID,
                            itemCode = i.ItemCode,
                            totalQty = n.Sum(x => x.RequestQty),
                            itemName = i.ItemName,
                            ItemUOM = i.UOM,
                            collectionTime = n.Select(x => x.Request.CollectionTime).First(),
                            requestIDs = n.Select(x => x.RequestID).ToList(),
                            deptId = n.Select(x=>x.Request.RequestByUser.DepartmentID).First()
                        }).ToList();
            return iter;
        }

        [HttpPost("{acceptedQty}/{retrievalID}")]
        public string allocateStationary(List<int> acceptedQty, int retrievalID)
        {

            //get the chunk of info passed to the View
            IEnumerable<dynamic> list = GetItemsByStatus("PendingDelivery", retrievalID);
            //create a dic: item code -- accptQty
            Dictionary<int, int> allocationList = new Dictionary<int, int>();
            foreach (var item in list)
            {
                allocationList.Add(item.ItemIds, 0);
            }

            for (int i = 1; i <= acceptedQty.Count(); i++)
            {
                allocationList[i] = acceptedQty[i - 1];
            }


            //List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
            List<RequestDetails> requestDetailsList = context123.RequestDetails.Where(x => x.Request.RetrievalID == retrievalID).ToList();


            //allocation starts
            for (int i = 0; i < allocationList.Count(); i++)
            {
                int reqQTY;
                int balance = allocationList.ElementAt(i).Value;

                List<int> requestIdList = list.Where(x => x.ItemIds == allocationList.ElementAt(i).Key)
                                            .Select(x => x.RequestIDs).FirstOrDefault();


                for (int j = 0; j < requestIdList.Count(); j++)
                {

                    //get the reqQTY of each request + item code
                    RequestDetails rr = requestDetailsList
                                    .Where(x => x.RequestID == requestIdList[j] && x.ItemID == allocationList.ElementAt(i).Key)
                                    .FirstOrDefault();
                    reqQTY = rr.RequestQty;

                    //check discrepancy
                    if (balance - reqQTY >= reqQTY)
                    {
                        balance -= reqQTY;
                        rr.ReceivedQty = reqQTY;
                    }
                    else if (balance - reqQTY < reqQTY && balance >= 0)
                    {
                        rr.ReceivedQty = balance;
                        balance = 0;
                    }
                }

                //after allocation done, change the status of the request
                foreach (int rqID in requestIdList)
                {
                    Request r = context123.Request.Where(x => x.RequestID == rqID).FirstOrDefault();
                    r.RequestStatus = EOrderStatus.Received;
                }
            }
            context123.SaveChanges(); //save all or nothing
            return "ok";
        }

        [HttpGet("{id}/{userId}/{collectionTime}/{fulfillQty}")]
        [Route("disburse-by-request/{id}/{userId}/{collectionTime}/{fulfillQty}")]
        public string DisburseByRequest(int id, int userId, string collectionTime, int fulfillQty)
        {
            //update request
            Request request = GetById(id);
            request.RequestStatus = EOrderStatus.PendingDelivery;
            request.CollectionTime = Convert.ToDateTime(collectionTime);
            request.ModifiedBy = userId;
            List<RequestDetails> reqItems = context123.RequestDetails.Where(x => x.RequestID == id).ToList();

            //To do: generate retrieval id
            

            //update fulfill qty of each request items
            for (int i = 0; i < reqItems.Count(); i++)
            {
                if (reqItems[i].FullfillQty == null && reqItems[i].RequestID == id)
                {
                    reqItems[i].FullfillQty = fulfillQty;
                    break;
                }
            }

            

            context123.SaveChanges();
            return "ok";
        }

        [HttpGet("{id}")]
        [Route("GetRequestByEmpId/{id}")]
        public IEnumerable<Request> GetRequestByEmpId(int id)
        {
            EOrderStatus packed = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), "Packed");
            EOrderStatus completed = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), "Completed");
            EOrderStatus pendingDelivery = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), "PendingDelivery");
            List<Request> request = context123.Request.Where(x => x.RequestBy == id && x.RequestStatus != packed && x.RequestStatus != completed && x.RequestStatus != pendingDelivery).ToList();
            return request;
        }

        [HttpGet]
        [Route("GetAllStatus")]
        public List<EOrderStatus> GetAllStatus()
        {

            List<EOrderStatus> st = Enum.GetValues(typeof(EOrderStatus)).Cast<EOrderStatus>().ToList();// Enum.GetValues(typeof(EOrderStatus)).Cast<EOrderStatus>();
            return st;
        }

    }

}
