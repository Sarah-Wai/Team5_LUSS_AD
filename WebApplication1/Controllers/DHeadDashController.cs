using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using LUSS_API.DB;
using LUSS_API.Models;
using LUSS_API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using static LUSS_API.Models.Status;
using Microsoft.AspNetCore.Http;




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

        [HttpGet("{deptID}")]
        [Route("get-top-cost-category/{deptID}")]
        public List<TopSixRequested> GetTopSummed(int deptID)
        {
            
            //NEED PASS HEAD'S DEPARTMENT FROM HIS USER SESSION
            int department = deptID;
            //NEED CHANGE STATUS TO COMPLETED WHEN DATA HAS COMPLETED
            var requestCat = (from requests in context123.Request
                                  join requestDetails in context123.RequestDetails on requests.RequestID equals requestDetails.RequestID
                                  join item in context123.Item on requestDetails.ItemID equals item.ItemID
                                  join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
                                  join user in context123.User on requests.RequestBy equals user.UserID
                                  where requests.RequestDate.Year == DateTime.Now.Year && requests.RequestStatus == EOrderStatus.Completed && user.DepartmentID == department
                                  select new TopSixRequested
                                  {
                                      ItemID = item.ItemID,
                                      ItemName = item.ItemCategory.CategoryName,
                                      Qty = requestDetails.ReceivedQty,
                                      ItemPrice = itemPrice.Price,
                                      TotalPrice = requestDetails.ReceivedQty * itemPrice.Price
                                  }).ToList();
            //NEED TO SELECT AND PASS TOP 6
            List<TopSixRequested> highestRequestCat = (requestCat.GroupBy(x => x.ItemName).Select(y => new TopSixRequested {

                ItemID = y.First().ItemID,
                ItemName = y.First().ItemName,
                Qty = y.Sum(s => s.Qty),
                ItemPrice = y.First().ItemPrice,
                TotalPrice = y.Sum(s=> s.TotalPrice)
              
            })).OrderByDescending(x => x.TotalPrice).Take(5).ToList();
                                         
            return highestRequestCat;            

        }

        [HttpGet("{deptID}")]
        [Route("get-request-breakdown/{deptID}")]
        public Dictionary<string, int> GetRequestBreakdown(int deptID)
        {
            //NEED PASS HEAD'S DEPARTMENT FROM HIS USER SESSION
            int department = deptID;
            var requestCat = from requests in context123.Request
                             join user in context123.User on requests.RequestBy equals user.UserID
                             where requests.RequestDate.Year == DateTime.Now.Year && user.DepartmentID == department
                             select requests;
            int approved = requestCat.Where(x => x.RequestStatus != EOrderStatus.Rejected || x.RequestStatus != EOrderStatus.Cancelled || x.RequestStatus != EOrderStatus.Pending).Count();
            int rejected = requestCat.Where(x => x.RequestStatus == EOrderStatus.Rejected).Count();
            int cancelled = requestCat.Where(x => x.RequestStatus == EOrderStatus.Cancelled).Count();
            int pending = requestCat.Where(x => x.RequestStatus == EOrderStatus.Pending).Count();



            Dictionary<string, int> requestBreakdown = new Dictionary<string, int>();
            requestBreakdown.Add("Rejected", rejected);
            requestBreakdown.Add("Approved", approved);
            requestBreakdown.Add("Cancelled", cancelled);
            if (pending != 0)
            {
                requestBreakdown.Add("Pending", pending);
            }

            return requestBreakdown;
        }

        [HttpGet("{deptID}")]
        [Route("get-department-cost/{deptID}")]
        public List<DHeadMonth> GetDepartmentCost(int deptID)
        {
            //NEED PASS HEAD'S DEPARTMENT FROM HIS USER SESSION
            int department = deptID;
            DateTime userSelected = new DateTime(2008, 1, 1, 6, 32, 0);
            DateTime today = DateTime.Now;
            DateTime startDate = DateTime.Now.AddMonths(-12);
            int year = startDate.Year;
            int month = startDate.Month;

            TimeSpan number = userSelected - today;
            int noOfMonths = ((today.Year - userSelected.Year)*12)+today.Month - userSelected.Month;

            Dictionary<int?, int?> deptMonthlyCost = new Dictionary<int?, int?>();
            Dictionary<int, Dictionary<int?, int?>> outputFormat = new Dictionary<int, Dictionary<int?, int?>>();

            //loop for each month
            for (int i = 0; i < 13; i++)
            {
                var requestsPriceMonth = (from requests in context123.Request
                                          join requestDetails in context123.RequestDetails on requests.RequestID equals requestDetails.RequestID
                                          join item in context123.Item on requestDetails.ItemID equals item.ItemID
                                          join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
                                          join user in context123.User on requests.RequestBy equals user.UserID
                                          where requests.CollectionTime.Year == year
                                          && requests.CollectionTime.Month == month
                                          && user.DepartmentID == department
                                          && requests.RequestStatus == EOrderStatus.Completed
                                          select new MonthlyCost
                                          {
                                              Date = requests.CollectionTime,
                                              ItemID = item.ItemID,
                                              ItemName = item.ItemName,
                                              Qty = requestDetails.ReceivedQty,
                                              ItemPrice = itemPrice.Price,
                                              TotalPrice = requestDetails.ReceivedQty * itemPrice.Price
                                          }).ToList();
               
                deptMonthlyCost.Add(i, requestsPriceMonth.Sum(s => s.TotalPrice)+1);


                month = month + 1;
                if(month == 13)
                {
                    year = year + 1;
                    month = 1;
                }
                 
                
            }

            outputFormat.Add(0, deptMonthlyCost);


            List<DHeadMonth> formattedForDisplay = new List<DHeadMonth>();
            //COMMENTED OUT SO CAN PUSH FIRST
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5555/predict");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            string receivedFromApi;
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(outputFormat));
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    receivedFromApi = streamReader.ReadLine();
                }
                string trimmed = receivedFromApi.Trim(new char[] { '[', ']' });
                string[] pythonReturned = trimmed.Split(',');
                int first = 13;
                int second = 0;

                foreach (string value in pythonReturned)
                {

                    double newInt = Convert.ToDouble(pythonReturned[second]);
                    int positive = Convert.ToInt32(newInt);
                    if (positive < 0)
                    {
                        positive = 1;
                    }
                    deptMonthlyCost.Add(first, positive);

                    first += 1;
                    second += 1;
                }
                // RETURN from API


                for (int i = 0; i < 12; i++)
                {
                    DHeadMonth working = new DHeadMonth();
                    working.Month = startDate.AddMonths(i).ToString("MMM");
                    working.YearOne = deptMonthlyCost.ElementAt(i).Value - 1;
                    working.YearTwo = deptMonthlyCost.ElementAt(i + 12).Value - 1;
                    formattedForDisplay.Add(working);

                }

            }
            catch (Exception ex)
            {
                for (int i = 0; i < 12; i++)
                {
                    DHeadMonth working = new DHeadMonth();
                    working.Month = startDate.AddMonths(i).ToString("MMM");
                    working.YearOne = deptMonthlyCost.ElementAt(i).Value - 1;
                    if (i == 0)
                    {
                        working.YearTwo = deptMonthlyCost.ElementAt(i + 12).Value - 1;
                    }
                    else
                    {
                        working.YearTwo = 0;
                    }
                    formattedForDisplay.Add(working);

                }
            }



            return formattedForDisplay;


        }






    }
}
