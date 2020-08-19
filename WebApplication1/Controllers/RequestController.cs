﻿using System;
using System.Collections.Generic;
using System.Linq;

using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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

        [HttpGet("get-request/{id}")]
        public Request GetById(int id)
        {
            Request request = context123.Request.Where(x => x.RequestID == id).FirstOrDefault();
            return request;
        }

      
        [HttpGet("{id}/{status}/{comment}")]
        [Route("ApproveRequestByDepHead/{id}/{status}/{comment}")]
        public string ApproveRequestByDepHead(int id,int status,string comment)
        {

            Request getRequest = context123.Request
                  .Where(x => x.RequestID == id).SingleOrDefault();
            if (getRequest != null)
            {
                getRequest.Comment = comment;
                if(status==1)
                getRequest.RequestStatus = EOrderStatus.Approved;
                else
                getRequest.RequestStatus = EOrderStatus.Rejected;
                context123.SaveChanges();
            }
            return "success";
        }

        [HttpGet]
        public IEnumerable<Request> Get()
        {
            List<Request> requestList = context123.Request.Where(x => x.RequestStatus != EOrderStatus.Rejected && x.RequestStatus != EOrderStatus.Pending).ToList();
            return requestList;
        }

        [HttpGet("{status}")]
        [Route("GetItemByStatus/{status}")]
        public IEnumerable<dynamic> GetItemsByStatus(string status)
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
                        select new
                        {
                            ItemIds = i.ItemID,
                            ItemCode = i.ItemCode,
                            TotalQty = n.Sum(x => x.RequestQty),
                            ItemName = i.ItemName,
                            ItemUOM = i.UOM,
                            CollectionTime = n.Select(x => x.Request.CollectionTime).First(),
                            RequestIDs = n.Select(x => x.RequestID).ToList()
                        }).ToList();
            return iter;
        }

        [HttpPost("{acceptedQty}")]
        public string allocateStationary(List<int> acceptedQty)
        {

            //get the chunk of info passed to the View
            IEnumerable<dynamic> list = GetItemsByStatus("PendingDelivery");
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


            List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
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
        [Route("dummy/{id}/{userId}/{collectionTime}/{fulfillQty}")]
        public string DisburseByRequest(int id, int userId , string collectionTime, int fulfillQty)
        {
            //update request
            Request request = GetById(id);
            request.RequestStatus = EOrderStatus.PendingDelivery;
            request.CollectionTime = Convert.ToDateTime(collectionTime);
            request.ModifiedBy = userId;
            List<RequestDetails> reqItems = context123.RequestDetails.Where(x => x.RequestID == id).ToList();

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

    }

}
