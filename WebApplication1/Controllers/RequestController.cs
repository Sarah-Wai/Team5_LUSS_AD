using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Castle.Core.Internal;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("{id}")]
        [Route("getAllRequestByDepID/{id}")]
        public IEnumerable<Request> GetAllRequest(int id)
        {
            List<Request> requests = context123.Request.Where(x => x.RequestByUser.DepartmentID == id).ToList();
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


        [HttpGet("{id}/{status}/{comment}")]
        [Route("ApproveRequestByDepHead/{id}/{status}/{comment}")]
        public Request ApproveRequestByDepHead(int id, int status,string comment)
        {

            Request getRequest = context123.Request
                  .Where(x => x.RequestID == id).SingleOrDefault();
            if (getRequest != null)
            {
                getRequest.Comment = comment;
                if (status == 1) 
                getRequest.RequestStatus = EOrderStatus.Approved;
                else
                getRequest.RequestStatus = EOrderStatus.Rejected;
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

        [HttpGet("{type}")]
        [Route("byType/{type}")]
        public IEnumerable<Request> GetByType(string type)
        {
            RequestType.ERequestType st = (RequestType.ERequestType)Enum.Parse(typeof(RequestType.ERequestType), type);
            List<Request> requestList = context123.Request.Where(x => x.RequestStatus == EOrderStatus.Approved && x.RequestType == st).ToList();
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
                            fullQty = n.Sum(x => x.FullfillQty),
                            rcvedQty = n.Sum(x => x.ReceivedQty),
                            itemName = i.ItemName,
                            itemUOM = i.UOM,
                            collectionTime = n.Select(x => x.Request.CollectionTime).First(),
                            requestIDs = n.Select(x => x.RequestID).ToList(),
                            deptId = n.Select(x => x.Request.RequestByUser.DepartmentID).First()
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
                int key = item.itemIds;
                int value = 0;
                allocationList.Add(key, value);
            }

            for (int i = 0; i < acceptedQty.Count(); i++)
            {
                int key = allocationList.ElementAt(i).Key;
                allocationList[key] = acceptedQty[i];
            }


            //List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
            List<RequestDetails> requestDetailsList = context123.RequestDetails.Where(x => x.Request.RetrievalID == retrievalID).ToList();


            //allocation starts
            for (int i = 0; i < allocationList.Count(); i++)
            {
                int reqQTY;
                int balance = allocationList.ElementAt(i).Value;

                List<int> requestIdList = list.Where(x => x.itemIds == allocationList.ElementAt(i).Key)
                                            .Select(x => x.requestIDs).FirstOrDefault();


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

        [HttpPost("{id}/{userId}/{collectionTime}/{fulfillQty}")]
        [Route("disburse-by-request/{id}/{userId}/{collectionTime}/{fulfillQty}")]
        public int DisburseByRequest(int id, int userId, string collectionTime, List<int> fulfillQty)
        {

            //To do: generate retrieval id , find out why is 0 when generated
            Retrieval retrieval = new Retrieval()
            {
                //RetrievalID = GetNewRetrievalId(), 
                IssueDate = DateTime.Now,
                Status = EOrderStatus.PendingDelivery
            };
            context123.Add(retrieval);
            context123.SaveChanges();

            //update request
            Request request = GetById(id);
            request.RequestStatus = EOrderStatus.PendingDelivery;
            request.CollectionTime = Convert.ToDateTime(collectionTime);
            request.ModifiedBy = userId;
            request.RetrievalID = retrieval.RetrievalID;

            List<RequestDetails> reqItems = context123.RequestDetails.Where(x => x.RequestID == id).ToList();

            //update fulfill qty of each request items
            for (int i = 0; i < reqItems.Count(); i++)
            {
                reqItems[i].FullfillQty = fulfillQty[i];
                reqItems[i].Item.InStockQty -= fulfillQty[i]; // less out stock
                //if (reqItems[i].FullfillQty == null && reqItems[i].RequestID == id)
                //{
                //    reqItems[i].FullfillQty = fulfillQty[i];
                //    break;
                //}
            }

            
            context123.SaveChanges();
            int userID = (int)request.RequestBy;

            return userID;
        }
        // get new retrieval Id
        //public int GetNewRetrievalId()
        //{
        //    int maxId = 0;
        //    int? currentId = context123.Retrieval.Max(x => x.RetrievalID);
        //    if (currentId != null)
        //    {
        //        maxId = (int)currentId;
        //    }
        //    return maxId + 1;
        //}

        [HttpGet("{id}")]
        [Route("GetRequestByEmpId/{id}")]
        public IEnumerable<Request> GetRequestByEmpId(int id)
        {
            EOrderStatus packed = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), "Packed");
            EOrderStatus completed = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), "Completed");
            EOrderStatus pendingDelivery = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), "PendingDelivery");
            List<Request> request = context123.Request.Where(x => x.RequestBy == id && x.RequestStatus != packed && x.RequestStatus != completed && x.RequestStatus != pendingDelivery).OrderByDescending(x=> x.RequestDate).ToList();
            //request.Select(x => x.RequestDetails.Where(a => a.isActive = true)); // remove cancelled request details
            
            return request;
        }

        [HttpGet]
        [Route("GetAllStatus")]
        public List<EOrderStatus> GetAllStatus()
        {
            List<EOrderStatus> st = Enum.GetValues(typeof(EOrderStatus)).Cast<EOrderStatus>().ToList();// Enum.GetValues(typeof(EOrderStatus)).Cast<EOrderStatus>();
            return st;
        }

        [HttpGet("deny/{reqID}")]
        public void RevertToPendingDelivery(int reqID)
        {
            Request r = context123.Request.Where(x => x.RequestID == reqID).FirstOrDefault();
            r.RequestStatus = EOrderStatus.PendingDelivery;
            context123.SaveChangesAsync(); 
        }

        [HttpGet("complete/{reqID}/{userID}")]
        public void CompleteOrder(int reqID, int userID)
        {
            Request r = context123.Request.Where(x => x.RequestID == reqID).FirstOrDefault();
            r.RequestStatus = EOrderStatus.Completed;
            checkDiscrepancy(reqID, userID);
            context123.SaveChangesAsync();
        }

        private void checkDiscrepancy(int reqID, int userID)
        {
            List<RequestDetails> requestDetails = context123.RequestDetails.Where(x => x.RequestID == reqID).ToList();
            Request currentRqt = context123.Request.First(x => x.RequestID == reqID);
            List<RequestDetails> dcp_RequestDetails = new List<RequestDetails>();


            //if there is discrepancy, create new Request, create RequestDetails

            foreach (var rq in requestDetails)
            {
                if(rq.FullfillQty < rq.RequestQty)
                {
                    dcp_RequestDetails.Add(rq);
                }
            }

            if (!dcp_RequestDetails.IsNullOrEmpty())
            {
                Request dcp_Request = new Request
                {
                    RequestStatus = EOrderStatus.Approved,
                    RequestDate = DateTime.Now,
                    RequestBy = currentRqt.RequestByUser.UserID,
                    RequestByUser = currentRqt.RequestByUser,
                    RequestType = RequestType.ERequestType.Discrepancy,
                    ModifiedBy = userID
                };
                context123.Request.Add(dcp_Request);
                context123.SaveChanges();
                AddRequestDetails(dcp_Request, dcp_RequestDetails);
            }
        }

        private void AddRequestDetails(Request dcp_Request, List<RequestDetails> dcp_RequestDetails)
        {
            int requestID = dcp_Request.RequestID;
            foreach (var rd in dcp_RequestDetails)
            {
                RequestDetails rqt_Details = new RequestDetails
                {
                    RequestQty = (int)(rd.RequestQty - rd.FullfillQty),
                    ItemID = rd.Item.ItemID,
                    isActive = true,
                    RequestID = requestID
                };
                context123.RequestDetails.Add(rqt_Details);
            }
            context123.SaveChanges();
        }

        [HttpGet("{id}")]
        [Route("CancelRequest/{id}")]
        public Request CancelRequest(int id)
        {
            Request getRequest = context123.Request
                  .Where(x => x.RequestID == id).SingleOrDefault();
            if (getRequest != null)
            {
                getRequest.RequestStatus = EOrderStatus.Cancelled;
                //List<RequestDetails> reqDetails = new  List<RequestDetails>();
                foreach (var reqDetail in getRequest.RequestDetails)
                {
                    if (reqDetail.RequestID == getRequest.RequestID)
                    {
                        reqDetail.isActive = false;
                    }
                }
                context123.SaveChanges();
            }
            return getRequest;
        }

        //create new request
        [HttpPost]
        [Route("UpdateRequestDetail")]
        public bool UpdateRequestDetail([FromBody] string jsonData)
        {
            bool isUpdated = false;
            Request req = new Request();
            if (jsonData != null)
            {
                //try create new order first

                try
                {
                    Request request = JsonConvert.DeserializeObject<Request>(jsonData);
                    ICollection<RequestDetails> reqDetailList = new List<RequestDetails>();

                    reqDetailList = context123.RequestDetails.Where(r => r.RequestID == request.RequestID).ToList();
                    
                    foreach (var reqDetail in reqDetailList)
                    {
                        foreach (var r in request.RequestDetails)
                        {
                            if (r.ItemID == reqDetail.ItemID)
                            {
                                reqDetail.RequestQty = r.RequestQty;
                                reqDetail.isActive = r.isActive;
                            }
                           
                        }
                    }
                    context123.SaveChanges();
                    isUpdated = true;
                }
                catch
                {
                    isUpdated = false;
                }
            }

            return isUpdated;
        }

        [HttpGet("{id}")]
        [Route("GetReqById/{id}")]
        public Request GetReqById(int id)
        {
            Request request = new Request();
            request = context123.Request.Where(x => x.RequestID == id).FirstOrDefault();
            //Request request = context123.Request.Select(x => x.RequestDetails.Select(y => y.isActive == false)).ToList();
            //List<RequestDetails> newReqDetail = new List<RequestDetails>();
            //foreach (var r in request.RequestDetails)
            //{
            //    if(r.isActive == true)
            //    {
            //        newReqDetail.Add(r);
            //    }
            //}
           // request.RequestDetails = newReqDetail;
            return request;
        }
    }
}