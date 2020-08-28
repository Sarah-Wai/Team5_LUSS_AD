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
using LUSS_API.Models.ViewModels;
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

        //[HttpGet("{id}")]
        //[Route("getAllRequestByDepID/{id}")]
        //public List<Request> GetAllRequest(int id)
        //{
        //    List<Request> requests = context123.Request.Where(x => x.RequestByUser.DepartmentID == id).ToList();
        //  //  string return_string= JsonConvert.SerializeObject(requests);
        //    return requests;

        //}

        [HttpGet("{id}")]
        [Route("getAllRequestByDepID/{id}")]
        public List<Request> GetAllRequest(int id)
        {
            List<Request> requests = context123.Request.Where(x => x.RequestByUser.DepartmentID == id).Select(c =>
            new Request()
            {
                RequestID = c.RequestID,
                RequestStatus = c.RequestStatus,
                RequestDate = c.RequestDate,
                RequestBy = c.RequestBy,
                ModifiedBy = c.ModifiedBy,
                Comment = c.Comment,
                RequestType = c.RequestType,
                ParentRequestID = c.ParentRequestID,
                CollectionTime = c.CollectionTime,
                RequestByUser = new User { UserID = c.RequestByUser.UserID, LastName = c.RequestByUser.LastName, FirstName = c.RequestByUser.FirstName },
                ModifiedByUser = c.ModifiedByUser == null ? new User { }:new User { UserID = c.RequestByUser.UserID, LastName = c.RequestByUser.LastName, FirstName = c.RequestByUser.FirstName },

            }).OrderByDescending(x => x.RequestDate).ToList();
            return requests;

        }

        [HttpGet("{status}")]
        [Route("GetRequestByStatus/{status}")]
        public IEnumerable<Request> GetRequestByStatus(string status)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
            List<Request> requestList = context123.Request.Where(x => x.RequestStatus == st).ToList();

            return requestList;
        }

        [HttpGet("{status}")]
        [Route("GetRequestCountByStatus/{status}")]
        public int GetRequestCountByStatus(string status)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
            int count = context123.Request.Where(x => x.RequestStatus == st).ToList().Count();

            return count;
        }

        [HttpGet("{status}/{deptId}")]
        [Route("GetRequestByStatusByDept/{status}/{deptId}")]
        public IEnumerable<Request> GetRequestByStatusByDept(string status, int deptId)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
            List<Request> requestList =  context123.Request.Where(x => x.RequestStatus == st && x.RequestByUser.DepartmentID == deptId).ToList();
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
        public Request ApproveRequestByDepHead(int id, int status, string comment)
        {
            string template = "";
            Request getRequest = context123.Request
                  .Where(x => x.RequestID == id).SingleOrDefault();
            if (getRequest != null)
            {
                getRequest.Comment = comment;
                if (status == 1)
                {
                    getRequest.RequestStatus = EOrderStatus.Approved;
                    template = "approvedVoucherToDeptHead";
                }
                else
                {
                    getRequest.RequestStatus = EOrderStatus.Rejected;
                    template = "rejectedVoucherToDeptHead";
                }
                context123.SaveChanges();
            }

            //Sending Email
            User toUser = context123.User.Where(x => x.UserID == getRequest.RequestBy).FirstOrDefault();
            EmailController.SendEmail(toUser.Email, toUser.FirstName + " " + toUser.LastName, template);

            return getRequest;
        }

        [HttpGet("{id}/{status}/{comment}")]
        [Route("ApproveRequestByDepHeadMB/{id}/{status}/{comment}")]
        public string ApproveRequestByDepHeadMB(int id, int status, string comment)
        {
            string template = "";
            Request getRequest = context123.Request.Where(x => x.RequestID == id).SingleOrDefault();
            try
            {
                if (getRequest != null)
                {
                    getRequest.Comment = comment;
                    if (status == 1)
                    {
                        getRequest.RequestStatus = EOrderStatus.Approved;
                        template = "approvedVoucherToDeptHead";
                    }
                    else
                    {
                        getRequest.RequestStatus = EOrderStatus.Rejected;
                        template = "rejectedVoucherToDeptHead";
                    }

                    context123.SaveChanges();

                    //Sending Email
                    User toUser = context123.User.Where(x => x.UserID == getRequest.RequestBy).FirstOrDefault();
                    EmailController.SendEmail(toUser.Email, toUser.FirstName + " " + toUser.LastName, template);

                }


                return "Success";
            }
            catch (Exception ex)
            {
                return "Fail";
            }
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
            if (acceptedQty != null && retrievalID != 0)
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
                if (list.Count() > 0)
                {
                    for (int i = 0; i < acceptedQty.Count(); i++)
                    {
                        int key = allocationList.ElementAt(i).Key;
                        allocationList[key] = acceptedQty[i];
                    }
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
            return "empty";
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

        [HttpGet("{id}")]
        [Route("GetRequestByEmpId/{id}")]
        public IEnumerable<Request> GetRequestByEmpId(int id)
        {
            EOrderStatus packed = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), "Packed");
            EOrderStatus completed = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), "Completed");
            EOrderStatus pendingDelivery = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), "PendingDelivery");
            List<Request> request = context123.Request.Where(x => x.RequestBy == id && x.RequestStatus != packed && x.RequestStatus != completed && x.RequestStatus != pendingDelivery).OrderByDescending(x => x.RequestDate).ToList();
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
                if (rq.FullfillQty < rq.RequestQty)
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

        [HttpGet("{reqId}/{reqDetail}")]
        [Route("RemoveRequestedItem/{reqId}/{reqDetail}")]
        public bool RemoveRequestedItem(int reqId, int reqDetail)
        {
            bool isRemoved = false;
            try
            {
                RequestDetails rd = context123.RequestDetails.Where(r => r.RequestDetailID.Equals(reqDetail) && r.RequestID.Equals(reqId)).FirstOrDefault();
                context123.RequestDetails.Remove(rd);
                context123.SaveChanges();
                isRemoved = true;
            }
            catch
            {
                isRemoved = false;
            }

            return isRemoved;
        }

        //for mobile
        [HttpGet("{status}")]
        [Route("get-request-by-status-mobile/{status}")]
        public IEnumerable<CustomRequest> MGetApprovedRequest(EOrderStatus status)
        {
            List<Request> requestList = context123.Request.Where(x => x.RequestStatus == status).ToList();
            List<CustomRequest> customRequests = new List<CustomRequest>();

            foreach (Request r in requestList)
            {
                CustomRequest c = new CustomRequest();
                c.RequestID = r.RequestID;
                c.RequestStatus = r.RequestStatus;
                c.RequestDate = r.RequestDate;
                c.RequestBy = r.RequestBy;
                c.ModifiedBy = r.ModifiedBy;
                c.Comment = r.Comment;
                c.RequestType = r.RequestType;
                c.ParentRequestID = r.ParentRequestID;
                c.CollectionTime = r.CollectionTime;
                c.RetrievalID = r.RetrievalID;
                c.RequestByName = r.RequestByUser.FirstName + " " + r.RequestByUser.LastName;
                c.ModifiedByName = r.ModifiedByUser.FirstName + " " + r.ModifiedByUser.LastName;
                c.DepartmentName = r.RequestByUser.Department.DepartmentName;
                User rep = context123.User.FirstOrDefault(x => x.DepartmentID == r.RequestByUser.DepartmentID && x.IsRepresentative == true);
                if (rep != null)
                {
                    c.DepartmentRep = rep.FirstName + " " + rep.LastName;
                }
                else c.DepartmentRep = null;
                c.CollectionPoint = r.RequestByUser.Department.CollectionPoint.Location;
                
                customRequests.Add(c);
            }
            return customRequests;
        }


        //mobile API
        [HttpGet("GetItemByRetrievalByDept/{retrId}/{deptId}")]
        public List<CustomRetrieval> GetItemByRetrievalBydept(int retrId, int deptId)
        {
            IEnumerable<dynamic> requests = GetItemsByStatus("PendingDelivery", retrId);
            IEnumerable<dynamic> requests_byDept = requests.Where(x => x.deptId == deptId).ToList();
            List<CustomRetrieval> retrievals = new List<CustomRetrieval>();

            foreach (var r in requests_byDept)
            {
                CustomRetrieval rt = new CustomRetrieval
                {
                    ItemID = r.itemIds,
                    ItemCode = r.itemCode,
                    ItemName = r.itemName,
                    UOM = r.itemUOM,
                    RequestedQty = r.totalQty
                };

                retrievals.Add(rt);
            }
            return retrievals;
        }

        [HttpGet("GetItemByRetrieval/{retrId}")]
        public List<CustomRetrieval> GetItemByRetrieval(int retrId)
        {
            IEnumerable<dynamic> requests = GetItemsByStatus("Received", retrId);
            List<CustomRetrieval> retrievals = new List<CustomRetrieval>();

            foreach (var r in requests)
            {
                CustomRetrieval rt = new CustomRetrieval
                {
                    ItemID = r.itemIds,
                    ItemCode = r.itemCode,
                    ItemName = r.itemName,
                    UOM = r.itemUOM,
                    RequestedQty = r.totalQty,
                    AcceptedQty = r.rcvedQty,
                    TotalQty = r.fullQty
                };

                retrievals.Add(rt);
            }
            return retrievals;
        }

        [HttpGet("{id}/{userId}/{collectionTime}/{fulfillQty}")]
        [Route("disburse-by-request-mobile/{id}/{userId}/{collectionTime}/{fulfillQty}")]
        public String MDisburseByRequest(int id, int userId, string collectionTime, string fulfillQty)
        {
            //parse string to array
            string[] separators = { ",", "[", "]" };
            string[] str = fulfillQty.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            List<int> qty = new List<int>();
            foreach (var s in str)
            {
                qty.Add(int.Parse(s));
            }

            DisburseByRequest(id, userId, collectionTime, qty);

            return "ok";
        }

        [HttpGet("{status}/{deptId}")]
        [Route("GetRequestMBByStatusByDept/{status}/{deptId}")]
        public IEnumerable<Request> GetRequestMBByStatusByDept(string status, int deptId)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);

            List<int> userIDs= context123.User.Where(x => x.DepartmentID == deptId).Select(c=>c.UserID).ToList();
            List<Request> requestList = (from r in context123.Request
                                         where r.RequestStatus == st
                                          && userIDs.Contains(r.RequestBy.Value)
                                         select r).ToList();
            List<Request> return_requestList = new List<Request>();
            foreach (Request r in requestList)
            {

                Request new_request = new Request()
                {

                    RequestID = r.RequestID,
                    RequestStatus = r.RequestStatus,
                    RequestDate = r.RequestDate,
                    RequestBy = r.RequestBy,
                    ModifiedBy = r.ModifiedBy,
                    Comment = r.Comment,
                    RequestType = r.RequestType,
                    ParentRequestID = r.ParentRequestID,
                    CollectionTime = r.CollectionTime,
                    RetrievalID = r.RetrievalID,
                    ModifiedByUser = new User { UserID = r.ModifiedByUser.UserID, FirstName = r.ModifiedByUser.FirstName, LastName = r.ModifiedByUser.LastName },
                    RequestByUser = new User { UserID = r.RequestByUser.UserID, FirstName = r.RequestByUser.FirstName, LastName = r.RequestByUser.LastName },
                    Retrieval = null

                };
                List<RequestDetails> details = PrepareForRequestDetail(r.RequestDetails);
                new_request.RequestDetails = details;
                return_requestList.Add(new_request);
            }


            return return_requestList;
        }

        public List<RequestDetails> PrepareForRequestDetail(ICollection<RequestDetails> details)
        {
            List<RequestDetails> return_list = new List<RequestDetails>();
            foreach (RequestDetails d in details)
            {
                RequestDetails requestDetails = new RequestDetails()
                {
                    RequestDetailID = d.RequestDetailID,
                    RequestQty = d.RequestQty,
                    ItemID = d.ItemID,
                    RequestID = d.RequestID,
                    FullfillQty = d.FullfillQty,
                    ReceivedQty = d.ReceivedQty,
                    isActive = d.isActive,
                    Item = new Item { ItemName = d.Item.ItemName, UOM = d.Item.UOM }


                };

                return_list.Add(requestDetails);

            }

            return return_list;
        }



        [HttpGet("Mobile_GetAccptQty/{acceptedQty}/{retrievalID}")]
        public void GetAllocateStationary(string acceptedQty, int retrievalID)
        {
            //parse string to array
            string[] separators = { ",", "[", "]" };
            string[] str = acceptedQty.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            List<int> qty = new List<int>();
            foreach (var s in str)
            {
                qty.Add(int.Parse(s));
            }

            allocateStationary(qty, retrievalID);

        }
    }

}