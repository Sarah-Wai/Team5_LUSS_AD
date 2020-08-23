using System;
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
    public class SupDashController : ControllerBase
    {

        public MyDbContext context123;
        private readonly ILogger<SupDashController> _logger;
        public SupDashController(ILogger<SupDashController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }
                
        
        [HttpGet]
        [Route("get-by-category")]
        public Dictionary<int, string> GetByCategory()
        {
            //connect all together
            //get category names
            //for each category, get each department's figures
                //save to array
                //add to dictionary of array

            

            Dictionary<int, string> deptNames = new Dictionary<int, string>();
            Dictionary<int, string> itemCategories = new Dictionary<int, string>();

            //foreach (var category in context123.ItemCategory)
            //{
            //    foreach (var department in context123.Department)
            //    {
            //        int sumDepartment = (from requests in context123.Request
            //                             join requestDetails in context123.RequestDetails on requests.RequestID equals requestDetails.RequestID
            //                             join item in context123.Item on requestDetails.ItemID equals item.ItemID
            //                             join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
            //                             join user in context123.User on requests.RequestBy equals user.UserID
            //                             where requests.CollectionTime.Year == DateTime.Now.Year
            //                             && user.DepartmentID == department.DepartmentID
            //                             && item.CategoryID == category.CategoryID
            //                             && requests.RequestStatus == EOrderStatus.Completed
                                         
            //                             select new { requestDetails.ReceivedQty * itemPrice.Price
            //    }
            //}

            deptNames = context123.Department.Select(x => new KeyValuePair<int, string>(x.DepartmentID, x.DepartmentCode)).ToDictionary(x => x.Key, x => x.Value);

            Dictionary<string, int[]> deptRequestsByCategory = new Dictionary<string, int[]>();

            foreach (KeyValuePair<int,string> entry in deptNames)
            {
                var catSum = (from requests in context123.Request
                              join requestDetails in context123.RequestDetails on requests.RequestID equals requestDetails.RequestID
                              join item in context123.Item on requestDetails.ItemID equals item.ItemID
                              join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
                              join user in context123.User on requests.RequestBy equals user.UserID
                              where requests.CollectionTime.Year == DateTime.Now.Year
                              && user.DepartmentID == entry.Key
                              && requests.RequestStatus == EOrderStatus.Completed
                              select new MonthlyCost
                              {                                  
                                  ItemID = item.CategoryID,
                                  ItemName = item.ItemName,
                                  Qty = requestDetails.ReceivedQty,
                                  ItemPrice = itemPrice.Price,
                                  TotalPrice = requestDetails.ReceivedQty * itemPrice.Price
                              }).ToList();

                List<MonthlyCost> highestRequestCat = (catSum.GroupBy(x => x.ItemID).Select(y => new MonthlyCost
                {
                    
                    ItemID = y.First().ItemID,
                    ItemName = y.First().ItemName,
                    Sum = y.Sum(s => s.TotalPrice),
                    ItemPrice = y.First().Date.Month,
                    TotalPrice = y.First().Date.Year
                })).ToList();



            }

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
                                         
            return deptNames;            

        }

        




    }
}
