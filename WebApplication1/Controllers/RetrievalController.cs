/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Castle.Core.Internal;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static LUSS_API.Models.Status;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RetrievalController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<RetrievalController> _logger;
        public RetrievalController(ILogger<RetrievalController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet("get/newRetrievalId")]
        public int GetNewRetrievalId()
        {
            int maxId = 0;
            int? currentId = context123.Retrieval.Max(x => x.RetrievalID);
            if (currentId != null)
            {
                maxId = (int)currentId;
            }
            return maxId + 1;
        }

        [HttpGet]
        public IEnumerable<Retrieval> GetRetrievals()
        {
            List<Retrieval> retrievals = context123.Retrieval.ToList();
            return retrievals;
        }

        [HttpGet("retrievalId/{id}")]
        public string RemoveRetrievalId(int id)
        {
            List<Request> requests = context123.Request
                .Where(x => x.RetrievalID == id).ToList();

            foreach(Request r in requests)
            {
                r.RetrievalID = null;
            }

            Delete(id);
            return "success";   
        }

 *//*       [HttpGet("{status}")]
        public IEnumerable<dynamic> GetRequestByStatus(string status)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
            List<Request> requests = context123.Request.ToList();
            List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
            List<Item> items = context123.Item.ToList();
            List<ItemPrice> prices = context123.ItemPrice.ToList();

            List<Request> approvedRequest = context123.Request
                .Where(x => x.RequestStatus.Equals(st)).ToList();

            Retrieval retrieval = new Retrieval()
            {
                RetrievalID = 1,
                Status = EOrderStatus.Approved,
                IssueDate = DateTime.Now

            };

            foreach (Request appReq in approvedRequest)
            {
                appReq.RetrievalID = retrieval.RetrievalID;
            }

 *//*           var iter = (from r in requests
                        join rd in requestDetailsList on r.RequestID equals rd.RequestID
                        where r.RequestStatus.Equals(st)
                        group rd by rd.ItemID into n
                        join i in items on n.FirstOrDefault().ItemID equals i.ItemID
                        select new
                        {
                            RetrievalID = retrieval.RetrievalID,
                            ItemID = i.ItemID,
                            ItemCode = i.ItemCode,
                            ItemName = i.ItemName,
                            UOM = i.UOM,
                            ItemPrice = prices.Where(x => x.ItemID == i.ItemID).FirstOrDefault().Price,
                            Location = i.StoreItemLocation,
                            InStock = i.InStockQty,
                            //RequestDetails = requestDetailsList.Where(x => x.Request.RequestStatus == st && x.ItemID == i.ItemID).ToList(), 
                            Category = i.ItemCategory.CategoryName,
                            TotalQty = n.Sum(x => x.RequestQty),
                            //RetrievedQty = n.Sum(x => x.FullfillQty)
                        }).ToList();
*//*
            
        //    return iter;
        }*//*

        [HttpGet("itemID/{id}")]
        public IEnumerable<dynamic> GetRequestDetailsById(int id)
        {
            List<Request> requests = context123.Request.ToList();
            List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
            List<Item> items = context123.Item.ToList();

            var iter = (from r in requests
                        join rd in requestDetailsList on r.RequestID equals rd.RequestID
                        where rd.ItemID == id
                        orderby r.RequestDate ascending
                        select new
                        {
                            RequestID = r.RequestID,
                            DepartmentCode = r.RequestByUser.Department.DepartmentCode,
                            DepartmentName = r.RequestByUser.Department.DepartmentName,
                            RequestedQty = rd.RequestQty,
                            UOM = rd.Item.UOM,
                            RequestedDate = r.RequestDate
                        }
                        ).ToList();

            return iter;

        }



        //// GET api/<controller>/5
        //[HttpGet("retrievalID/{id}")]
        //public IEnumerable<dynamic> GetRetrivalDetailsById(int id)
        //{
        //    List<Request> requests = context123.Request.ToList();
        //    List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
        //    List<Item> items = context123.Item.ToList();
        //    List<ItemPrice> prices = context123.ItemPrice.ToList();

        //    var iter = (from r in requests
        //                join rd in requestDetailsList on r.RequestID equals rd.RequestID
        //                where r.RetrievalID == id
        //                group rd by rd.ItemID into n
        //                join i in items on n.FirstOrDefault().ItemID equals i.ItemID
        //                select new
        //                {
        //                    ItemID = i.ItemID,
        //                    ItemCode = i.ItemCode,
        //                    ItemName = i.ItemName,
        //                    UOM = i.UOM,
        //                    ItemPrice = prices.Where(x => x.ItemID == i.ItemID).FirstOrDefault().Price,
        //                    Location = i.StoreItemLocation,
        //                    InStock = i.InStockQty,
        //                    RequestDetails = requestDetailsList.Where(x => x.Request.RetrievalID == id && x.ItemID == i.ItemID).ToList(),
        //                    Category = i.ItemCategory.CategoryName,
        //                    TotalQty = n.Sum(x => x.RequestQty),
        //                    RetrievedQty = n.Sum(x => x.FullfillQty)
        //                }).ToList();
        //    return iter;

        //}

        //[HttpPost("{acceptedQty}")]
        //public string allocateStationary(List<int> retrievedQty)
        //{

        //    //get the chunk of info passed to the View
        //    IEnumerable<dynamic> list = GetRequestByStatus("Packed");
        //    //create a dic: item code -- retrievedQth
        //    Dictionary<int, int> allocationList = new Dictionary<int, int>();
        //    foreach (var item in list)
        //    {
        //        allocationList.Add(item.ItemIds, 0);
        //    }

        //    for (int i = 1; i <= retrievedQty.Count(); i++)
        //    {
        //        allocationList[i] = retrievedQty[i - 1];
        //    }


        //    List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
        //    //allocation starts
        //    for (int i = 0; i < allocationList.Count(); i++)
        //    {
        //        int reqQTY;
        //        int balance = allocationList.ElementAt(i).Value;

        //        List<int> requestIdList = list.Where(x => x.ItemIds == allocationList.ElementAt(i).Key)
        //                                    .Select(x => x.RequestIDs).FirstOrDefault();


        //        for (int j = 0; j < requestIdList.Count(); j++)
        //        {

        //            //get the reqQTY of each request + item code
        //            RequestDetails rr = requestDetailsList
        //                            .Where(x => x.RequestID == requestIdList[j] && x.ItemID == allocationList.ElementAt(i).Key)
        //                            .FirstOrDefault();
        //            reqQTY = rr.RequestQty;

        //            //check discrepancy
        //            if (balance - reqQTY >= reqQTY)
        //            {
        //                balance -= reqQTY;
        //                rr.ReceivedQty = reqQTY;
        //            }
        //            else if (balance - reqQTY < reqQTY && balance >= 0)
        //            {
        //                rr.ReceivedQty = balance;
        //                balance = 0;
        //            }
        //        }

        //        //after allocation done, change the status of the request
        //        foreach (int rqID in requestIdList)
        //        {
        //            Request r = context123.Request.Where(x => x.RequestID == rqID).FirstOrDefault();
        //            r.RequestStatus = EOrderStatus.Received;
        //        }
        //    }
        //    context123.SaveChanges(); //save all or nothing
        //    return "ok";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Retrieval retrieval = context123.Retrieval.FirstOrDefault(r => r.RetrievalID == id);
            context123.Retrieval.Remove(retrieval);
            context123.SaveChanges();
        }
    }

*/