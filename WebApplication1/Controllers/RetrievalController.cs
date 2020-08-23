using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Castle.Core.Internal;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("{status}")]
        [Route("byStatus/{status}")]
        public IEnumerable<dynamic> GetRequestByStatus(string status)
        {
            EOrderStatus st = (EOrderStatus)Enum.Parse(typeof(EOrderStatus), status);
            List<Request> requests = context123.Request.ToList();
            List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
            List<Item> items = context123.Item.ToList();
            List<ItemPrice> prices = context123.ItemPrice.ToList();

            List<Request> approvedRequest = context123.Request
                .Where(x => x.RequestStatus.Equals(st)).ToList();

            if(approvedRequest.Count != 0)
            {
                Retrieval retrieval = new Retrieval()
                {
                    //RetrievalID = GetNewRetrievalId(),// for testing, to be removed
                    Status = EOrderStatus.Approved,
                    IssueDate = DateTime.Now
                };

                context123.Retrieval.Add(retrieval);
                context123.SaveChanges();

                foreach (Request appReq in approvedRequest)
                {
                    appReq.RetrievalID = retrieval.RetrievalID;
                }

                context123.SaveChanges();

                var iter = (from r in requests
                            join rd in requestDetailsList on r.RequestID equals rd.RequestID
                            where r.RequestStatus.Equals(st)
                            group rd by rd.ItemID into n
                            join i in items on n.FirstOrDefault().ItemID equals i.ItemID
                            select new
                            {
                                RetrievalID = retrieval.RetrievalID,
                                ReorderLevel = i.ReStockLevel,
                                ItemID = i.ItemID,
                                ItemCode = i.ItemCode,
                                ItemName = i.ItemName,
                                UOM = i.UOM,
                                ItemPrice = prices.Where(x => x.ItemID == i.ItemID).FirstOrDefault().Price,
                                Location = i.StoreItemLocation,
                                InStock = i.InStockQty,
                                Category = i.ItemCategory.CategoryName,
                                TotalQty = n.Sum(x => x.RequestQty),
                            }).ToList();


                return iter;
            }
            return null;
            
        }

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

        [HttpGet("byRetrieval/{id}")]
        public IEnumerable<dynamic> GetUniqueItemByRetrievalId(int id)
        {
            List<Request> requests = context123.Request.ToList();
            List<RequestDetails> requestDetailsList = context123.RequestDetails.ToList();
            List<Item> items = context123.Item.ToList();

            var iter = (from r in requests
                        join rd in requestDetailsList on r.RequestID equals rd.RequestID
                        where r.RetrievalID == id
                        group rd by rd.ItemID into n
                        join i in items on n.FirstOrDefault().ItemID equals i.ItemID
                        select new
                        {
                            ItemID = i.ItemID,
                            ItemCode = i.ItemCode,
                            ItemName = i.ItemName,
                            UOM = i.UOM,
                            Location = i.StoreItemLocation,
                            InStock = i.InStockQty, 
                            Category = i.ItemCategory.CategoryName,
                            TotalQty = n.Sum(x => x.RequestQty),
                        }).ToList();


            return iter;

        }


        [HttpPost("{retrievedQty}/{retrievalId}/{collectionDate}/{id}")]
        public List<User> allocateFulfilledQty(List<int> retrievedQty, int retrievalId, string collectionDate, int id)
        {
        
            List<RequestDetails> requests = context123.RequestDetails
                .Where(x => x.Request.RetrievalID == retrievalId).ToList();

            List<int> items = requests.Select(x => x.ItemID).Distinct().ToList();

            IEnumerable<dynamic> uniqueItems = GetUniqueItemByRetrievalId(retrievalId);

            Dictionary<int, int> retrievedQtyList = new Dictionary<int, int>();
            foreach(var u in uniqueItems)
            {
                retrievedQtyList.Add(u.ItemID, 0);
            }

            for(int i = 0; i < retrievedQty.Count(); i++)
            {
                retrievedQtyList[retrievedQtyList.ElementAt(i).Key] = retrievedQty[i];
            }

            //List<int> items = requests.Select(x => x.ItemID).ToList();

            foreach(int u in items)
            {
                List<RequestDetails> reqByItem = requests.Where(x => x.ItemID == u).OrderBy(x => x.Request.RequestDate).ToList();

                int retQty = retrievedQtyList[u];
                Item item = context123.Item.Where(x => x.ItemID == u).FirstOrDefault();

                for(int k = 0; k < reqByItem.Count(); k++)
                {
                    int reqQty = reqByItem[k].RequestQty;

                    if(retQty >= reqQty && !(retQty<= 0))
                    {
                        reqByItem[k].FullfillQty = reqQty;
                        item.InStockQty = item.InStockQty - reqQty;
                   
                    }
                    if(retQty < reqQty && !(retQty <= 0))
                    {
                        reqByItem[k].FullfillQty = retQty;
                        item.InStockQty = item.InStockQty - retQty;
                       
                    }

                     retQty = retQty - reqQty;
                    
                }
                
            }

            context123.SaveChanges();

            foreach ( RequestDetails r in requests)
            {
                if(r.FullfillQty != 0 && r.FullfillQty != null)
                {
                    r.Request.CollectionTime = Convert.ToDateTime(collectionDate);
                    r.Request.RequestStatus = EOrderStatus.PendingDelivery;
                    r.Request.ModifiedBy = id;
                }
                else if(r.FullfillQty == null)
                {
                    //r.Request.RetrievalID = null;
                    r.FullfillQty = null;
                }
            }

            Retrieval retrieval = context123.Retrieval.Where(x => x.RetrievalID == retrievalId).FirstOrDefault();
            retrieval.Status = EOrderStatus.PendingDelivery;

            context123.SaveChanges(); //save all or nothing

            List<User> users = requests.Select(x => x.Request.RequestByUser).Distinct().ToList();
            return users;
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
}