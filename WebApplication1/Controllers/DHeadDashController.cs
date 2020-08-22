using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class DHeadDashController : ControllerBase
    {


        public MyDbContext context123;
        private readonly ILogger<DHeadDashController> _logger;
        public DHeadDashController(ILogger<DHeadDashController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }
        

        //[HttpGet]
        //[Route("get-user-name")]
        //public string GetName()
        //{            
        //    int dummyID = 1;//TO replace by real ID
        //    User currentUser = context123.User.Where(x => x.UserID == dummyID).FirstOrDefault();
            
        //    return currentUser.FirstName.ToString();

        //}
        [HttpGet]
        [Route("get-top-cost-category")]
        public List<TopSixRequested> GetTopSummed()
        {
            //NEED PASS HEAD'S DEPARTMENT FROM HIS USER SESSION
            int department = 1;
            //NEED CHANGE STATUS TO COMPLETED WHEN DATA HAS COMPLETED
            var requestCat = (from requests in context123.Request
                                  join requestDetails in context123.RequestDetails on requests.RequestID equals requestDetails.RequestID
                                  join item in context123.Item on requestDetails.ItemID equals item.ItemID
                                  join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
                                  join user in context123.User on requests.RequestBy equals user.UserID
                                  where requests.RequestDate.Year == DateTime.Now.Year && requests.RequestStatus == EOrderStatus.Approved && user.DepartmentID == department
                                  select new TopSixRequested
                                  {
                                      ItemID = item.ItemID,
                                      ItemName = item.ItemCategory.CategoryName,
                                      Qty = requestDetails.ReceivedQty,
                                      ItemPrice = itemPrice.Price,
                                      TotalPrice = requestDetails.ReceivedQty * itemPrice.Price
                                  }).OrderBy(x=>x.TotalPrice).ToList();
            //NEED TO SELECT AND PASS TOP 6
            List<TopSixRequested> highestRequestCat = (requestCat.GroupBy(x => x.ItemName).Select(y => new TopSixRequested {

                ItemID = y.First().ItemID,
                ItemName = y.First().ItemName,
                Qty = y.Sum(s => s.Qty),
                ItemPrice = y.First().ItemPrice,
                TotalPrice = y.Sum(s=> s.TotalPrice)
              
            })).ToList();
                                         
            return highestRequestCat;            

        }

        [HttpGet]
        [Route("get-request-breakdown")]
        public Dictionary<string, int> GetRequestBreakdown()
        {
            //NEED PASS HEAD'S DEPARTMENT FROM HIS USER SESSION
            int department = 1;
            var requestCat = from requests in context123.Request
                              join user in context123.User on requests.RequestBy equals user.UserID
                              where requests.RequestDate.Year == DateTime.Now.Year && user.DepartmentID == department
                              select requests;
            int approved = requestCat.Where(x => x.RequestStatus == EOrderStatus.Approved).Count();
            int rejected = requestCat.Where(x => x.RequestStatus == EOrderStatus.Rejected).Count();



            Dictionary<string, int> requestBreakdown = new Dictionary<string, int>();
            requestBreakdown.Add("Approved", approved);
            requestBreakdown.Add("Rejected", rejected);         


            return requestBreakdown;
        }




    }
}
