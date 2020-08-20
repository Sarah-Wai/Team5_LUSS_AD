﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using LUSS_API.DB;
using LUSS_API.Models;
using LUSS_API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using static LUSS_API.Models.Status;

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClerkDashController : ControllerBase
    {

        public MyDbContext context123;
        private readonly ILogger<ClerkDashController> _logger;
        public ClerkDashController(ILogger<ClerkDashController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }
        

        [HttpGet]
        [Route("get-user-name")]
        public string GetName()
        {            
            int dummyID = 1;//TO replace by real ID
            User currentUser = context123.User.Where(x => x.UserID == dummyID).FirstOrDefault();
            
            return currentUser.FirstName.ToString();

        }
        [HttpGet]
        [Route("get-top-summed")]
        public List<TopSixRequested> GetTopSummed()
        {
            
            var highestRequest = (from requests in context123.Request
                                  join requestDetails in context123.RequestDetails on requests.RequestID equals requestDetails.RequestID
                                  join item in context123.Item on requestDetails.ItemID equals item.ItemID
                                  join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
                                  where requests.RequestDate.Year == DateTime.Now.Year && requests.RequestStatus == EOrderStatus.Approved
                                  select new TopSixRequested
                                  {
                                      ItemID = requestDetails.ItemID,
                                      ItemName = item.ItemName,
                                      Qty = requestDetails.ReceivedQty,
                                      ItemPrice = itemPrice.Price,
                                      TotalPrice = requestDetails.ReceivedQty * itemPrice.Price
                                  }).OrderBy(x=>x.TotalPrice).ToList();

            List<TopSixRequested> itemSum = (highestRequest.GroupBy(x => x.ItemID).Select(y => new TopSixRequested {

                ItemID = y.First().ItemID,
                ItemName = y.First().ItemName,
                Qty = y.Sum(s => s.Qty),
                ItemPrice = y.First().ItemPrice,
                TotalPrice = y.Sum(s=> s.TotalPrice)
              
            })).ToList();
                                         
            return itemSum;            

        }

        [HttpGet]
        [Route("get-clerk-pending")]
        public int GetClerkPendingAdjustment()
        {
            int pendingAdjustments = 0;
            pendingAdjustments = context123.AdjustmentVouncher.Where(x => x.Status == AdjustmentVoucherStatus.AdjustmentStatus.Pending).Count();
            
            return pendingAdjustments;

        }

        [HttpGet]
        [Route("get-next-collection-datetime")]
        public DateTime GetNextCollectionDate()
        {
            // get next request
            Request nextDelivery = context123.Request.Where(x => x.RequestStatus == EOrderStatus.PendingDelivery).OrderBy(y => y.CollectionTime).First();
            
            DateTime collectionTime = nextDelivery.CollectionTime;


            return collectionTime;
        }
        [HttpGet]
        [Route("get-next-collection-time-location")]
        public CollectionPoint GetNextCollectionTimeLocation()
        {
            // get next request
            Request nextDelivery = context123.Request.Where(x => x.RequestStatus == EOrderStatus.PendingDelivery).OrderBy(y => y.CollectionTime).First();
            User requestMaker = context123.User.Where(x => x.UserID == nextDelivery.RequestBy).FirstOrDefault();
            Department department = context123.Department.Where(x => x.DepartmentID == requestMaker.DepartmentID).FirstOrDefault();
            CollectionPoint collectionPoint = context123.CollectionPoint.Where(x => x.CollectionPointID == department.CollectionPointID).FirstOrDefault();

            
            return collectionPoint;
        }

        [HttpGet]
        [Route("get-low-stock-item-count")]
        public int GetLowStockItemCount()
        {
            // get next request
            
            int lowStockItemCount = context123.Item.Where(x => x.InStockQty < x.ReStockLevel).Count();


            return lowStockItemCount;
        }




    }
}
