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
        [Route("get-by-department-category")]
        public List<CategoryActorSum> GetEachDepartmentCategory()
        {
            var ungroupedDepartmentCategory = (from requests in context123.Request
                                 join requestDetails in context123.RequestDetails on requests.RequestID equals requestDetails.RequestID
                                 join item in context123.Item on requestDetails.ItemID equals item.ItemID
                                 join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
                                 join user in context123.User on requests.RequestBy equals user.UserID
                                 where requests.CollectionTime.Year == DateTime.Now.Year
                                 && requests.RequestStatus == EOrderStatus.Completed
                                 select new CategoryActorSum
                                 {
                                     Category = item.ItemCategory.CategoryName,
                                     Actor = user.Department.DepartmentName,
                                     Sum = requestDetails.ReceivedQty * itemPrice.Price
                                 }).OrderBy(x => x.Category).ToList();

            var departmentCategory = ungroupedDepartmentCategory
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

            var uniqueCategories = departmentCategory.Select(dc => new { dc.Category }).Distinct().OrderBy(x => x.Category);
            var uniqueDepts = departmentCategory.Select(dc => new { dc.Actor }).Distinct().OrderBy(x => x.Actor);


            return departmentCategory;

        }

        [HttpGet]
        [Route("get-by-supplier-category")]
        public List<CategoryActorSum> GetEachSupplierCategory()
        {
            var ungroupedSupplierCategory = (from po in context123.PurchaseOrder
                                               join poItems in context123.PurchaseOrderItems on po.POID equals poItems.POID
                                               join item in context123.Item on poItems.ItemID equals item.ItemID
                                               join itemPrice in context123.ItemPrice on item.ItemID equals itemPrice.ItemID
                                               join supplier in context123.Supplier on po.SupplierID equals supplier.SupplierID
                                               where po.CreatedOn.Year == DateTime.Now.Year
                                               && po.Status == POStatus.Completed
                                               && supplier.SupplierID == itemPrice.SupplierID
                                               select new CategoryActorSum
                                               {
                                                   Category = item.ItemCategory.CategoryName,
                                                   Actor = supplier.SupplierName,
                                                   Sum = poItems.ReceivedQty * itemPrice.Price
                                               }).OrderBy(x => x.Category).ToList();

            var supplierCategory = ungroupedSupplierCategory
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

            var uniqueCategories = supplierCategory.Select(dc => new { dc.Category }).Distinct().OrderBy(x => x.Category);
            var uniqueDepts = supplierCategory.Select(dc => new { dc.Actor }).Distinct().OrderBy(x => x.Actor);


            return supplierCategory;

        }






    }
}
