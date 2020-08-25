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
using static LUSS_API.Models.PurchaseOrderStatus;
using static LUSS_API.Models.Status;

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        int twoAgo = DateTime.Now.AddMonths(-2).Month;

        public MyDbContext context123;
        private readonly ILogger<ReportController> _logger;
        public ReportController(ILogger<ReportController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }
                
        
        [HttpGet]
        [Route("get-month-on-month-dept")]
        public List<CategoryActorSum> GetDepartmentMonths()
        {
            var ungroupedDepartmentMonth = (from requests in context123.Request
                                 join requestDetails in context123.RequestDetails on requests.RequestID equals requestDetails.RequestID
                                 join item in context123.Item on requestDetails.ItemID equals item.ItemID
                                 join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
                                 join user in context123.User on requests.RequestBy equals user.UserID
                                 where requests.CollectionTime.Year == DateTime.Now.Year
                                 && requests.RequestStatus == EOrderStatus.Completed
                                 select new CategoryActorSum
                                 {
                                     Category = requests.CollectionTime.ToString("MMM"),
                                     Actor = user.Department.DepartmentName,
                                     Sum = requestDetails.ReceivedQty * itemPrice.Price
                                 }).ToList();

            var departmentMonth = ungroupedDepartmentMonth
                .GroupBy(x => new
                {
                    x.Category,
                    x.Actor,
                })
                .Select(n => new CategoryActorSum()
                {
                    Category = n.Key.Category,
                    Actor = n.Key.Actor,
                    Sum = n.Sum(x => x.Sum)
                }).ToList();

            var months = departmentMonth.Select(dc => new { dc.Category }).Distinct().OrderBy(x => x.Category);
            var uniqueDepts = departmentMonth.Select(dc => new { dc.Actor }).Distinct().OrderBy(x => x.Actor);


            return departmentMonth;

        }

        [HttpGet]
        [Route("get-month-on-month-supplier")]
        public List<CategoryActorSum> GetSupplierMonths()
        {
            var ungroupedSupplierMonth = (from po in context123.PurchaseOrder
                                               join poItems in context123.PurchaseOrderItems on po.POID equals poItems.POID
                                               join item in context123.Item on poItems.ItemID equals item.ItemID
                                               join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
                                               join supplier in context123.Supplier on po.SupplierID equals supplier.SupplierID
                                               where po.CreatedOn.Year == DateTime.Now.Year
                                               && (po.CreatedOn.Month == DateTime.Now.AddMonths(-2).Month || po.CreatedOn.Month == DateTime.Now.AddMonths(-1).Month || po.CreatedOn.Month == DateTime.Now.Month)
                                               && po.Status == POStatus.Completed
                                               && supplier.SupplierID == itemPrice.SupplierID
                                               select new CategoryActorSum
                                               {
                                                   Category = po.CreatedOn.ToString("MMM"),
                                                   Actor = supplier.SupplierName,
                                                   Sum = poItems.ReceivedQty * itemPrice.Price
                                               }).ToList();

            var supplierMonth = ungroupedSupplierMonth
                .GroupBy(x => new
                {
                    x.Category,
                    x.Actor,
                })
                .Select(n => new CategoryActorSum()
                {
                    Category = n.Key.Category,
                    Actor = n.Key.Actor,
                    Sum = n.Sum(x => x.Sum)
                }).ToList();



            return supplierMonth;

        }







    }
}
