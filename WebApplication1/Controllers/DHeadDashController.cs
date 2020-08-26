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
            int approved = requestCat.Where(x => x.RequestStatus == EOrderStatus.Approved).Count();
            int rejected = requestCat.Where(x => x.RequestStatus == EOrderStatus.Rejected).Count();
            int cancelled = requestCat.Where(x => x.RequestStatus == EOrderStatus.Cancelled).Count();
            int pending = requestCat.Where(x => x.RequestStatus == EOrderStatus.Pending).Count();



            Dictionary<string, int> requestBreakdown = new Dictionary<string, int>();
            requestBreakdown.Add("Rejected", rejected);
            requestBreakdown.Add("Approved", approved);
            requestBreakdown.Add("Cancelled", cancelled);
            requestBreakdown.Add("Pending", pending);

            //int department = 1;
            //var requestCat = (from requests in context123.Request
            //                  join user in context123.User on requests.RequestBy equals user.UserID
            //                  where requests.RequestDate.Year == DateTime.Now.Year && user.DepartmentID == department
            //                  && (requests.RequestStatus == EOrderStatus.Approved || requests.RequestStatus == EOrderStatus.Rejected
            //                  || requests.RequestStatus == EOrderStatus.Cancelled)
            //                  select requests).ToList();

            //List<StatusCount> requestBreakdown = requestCat.GroupBy(x => x.RequestStatus).Select(y => new StatusCount
            //{
            //    Status = y.First().RequestStatus.ToString(),
            //    Count = y.Count()
            //}).ToList();

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
                //List<MonthlyCost> highestRequestCat = (requestsPriceMonth.GroupBy(x => x.Date.Month).Select(y => new MonthlyCost
                //{
                //    Date = y.First().Date,
                //    ItemID = y.First().Date.Month,
                //    ItemName = y.First().ItemName,
                //    Sum = y.Sum(s => s.TotalPrice),
                //    ItemPrice = y.First().Date.Month,
                //    TotalPrice = y.First().Date.Year
                //})).ToList();

                //test naming
                //deptMonthlyCost.Add(year*100+month, requestsPriceMonth.Sum(s => s.TotalPrice));
                deptMonthlyCost.Add(i, requestsPriceMonth.Sum(s => s.TotalPrice)+1);




                month = month + 1;
                if(month == 13)
                {
                    year = year + 1;
                    month = 1;
                }
                 
                
            }

            outputFormat.Add(0, deptMonthlyCost);

            //CODE TO TEST IF CAN SEND
            //Dictionary<int?, int?> test = new Dictionary<int?, int?>();
            //Dictionary<int, Dictionary<int?, int?>> testFormat = new Dictionary<int, Dictionary<int?, int?>>();
            //test.Add(0, 1); test.Add(1, 12); test.Add(2, 12); test.Add(3, 12); test.Add(4, 12); test.Add(5, 0); test.Add(6, 12); test.Add(7, 12); test.Add(8, 12); test.Add(9, 12); test.Add(10, 12); test.Add(11, 12); test.Add(12, 12);
            //testFormat.Add(0, test);

            List<DHeadMonth> formattedForDisplay = new List<DHeadMonth>();

            //COMMENTED OUT SO CAN PUSH FIRST
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5555/predict");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            string receivedFromApi;
            try {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    //string json = JsonConvert.SerializeObject(testFormat);

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
                        positive = positive * (-1);
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
                    working.YearTwo = 0;
                    formattedForDisplay.Add(working);

                }
            }

         
        
          


            return formattedForDisplay;


        }

        // >>>> BELOW CODE GETS MONTH AFTER MONTH, BUT IF THERE ARE MONTHS WILL 0 COMPLETED, MONTHS WILL BE SKIPPED. KEPT IN CASE

        //[HttpGet]
        //[Route("get-department-cost")]
        //public Dictionary<int?, int?> GetDepartmentCost()
        //{
        //    //NEED PASS HEAD'S DEPARTMENT FROM HIS USER SESSION
        //    int department = 1;
        //    DateTime userSelected = new DateTime(2008, 1, 1, 6, 32, 0);
        //    DateTime today = DateTime.Now;
        //    DateTime startDate = DateTime.Now.AddMonths(-12);
        //    int year = startDate.Year;
        //    int month = startDate.Month;
        //    int monthCounter = 0;

        //    TimeSpan number = userSelected - today;
        //    int noOfMonths = ((today.Year - userSelected.Year) * 12) + today.Month - userSelected.Month;

        //    Dictionary<int?, int?> deptMonthlyCost = new Dictionary<int?, int?>();

        //    //loop for each year
        //    for (int i = 0; i < 3; i++)
        //    {
        //        var requestsPriceMonth = (from requests in context123.Request
        //                                  join requestDetails in context123.RequestDetails on requests.RequestID equals requestDetails.RequestID
        //                                  join item in context123.Item on requestDetails.ItemID equals item.ItemID
        //                                  join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
        //                                  join user in context123.User on requests.RequestBy equals user.UserID
        //                                  where requests.CollectionTime.Year == year
        //                                  && user.DepartmentID == department
        //                                  && requests.RequestStatus == EOrderStatus.Completed
        //                                  select new MonthlyCost
        //                                  {
        //                                      Date = requests.CollectionTime,
        //                                      ItemID = item.ItemID,
        //                                      ItemName = item.ItemName,
        //                                      Qty = requestDetails.ReceivedQty,
        //                                      ItemPrice = itemPrice.Price,
        //                                      TotalPrice = requestDetails.ReceivedQty * itemPrice.Price
        //                                  }).ToList();
        //        List<MonthlyCost> highestRequestCat = (requestsPriceMonth.GroupBy(x => x.Date.Month).Select(y => new MonthlyCost
        //        {
        //            Date = y.First().Date,
        //            ItemID = y.First().Date.Month,
        //            ItemName = y.First().ItemName,
        //            Sum = y.Sum(s => s.TotalPrice),
        //            ItemPrice = y.First().Date.Month,
        //            TotalPrice = y.First().Date.Year
        //        })).ToList();

        //        foreach (MonthlyCost mc in highestRequestCat)
        //        {
        //            deptMonthlyCost.Add(monthCounter, mc.Sum);
        //            monthCounter = monthCounter + 1;
        //            //break after getting number of months required
        //            if (monthCounter == 2)
        //                break;
        //        }
        //        //break after getting number of months required
        //        if (monthCounter == 2)
        //            break;
        //        year = year + 1;

        //    }
        //    return deptMonthlyCost;
        //}







    }
}
