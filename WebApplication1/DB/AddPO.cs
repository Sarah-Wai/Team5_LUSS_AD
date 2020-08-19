using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LUSS_API.Models.PurchaseOrderStatus;

namespace LUSS_API.DB
{
    public class AddPO
    {
        public static List<PurchaseOrder> getAllPO()
        {
            List<PurchaseOrder> purchases = new List<PurchaseOrder>();

            PurchaseOrder p1 = new PurchaseOrder()
            {
                //POID = 1,
                CreatedOn = DateTime.Now,
                ExpectedDate = DateTime.Now,
                PurchasedBy = 1,
                SupplierID = 1,
                Status = POStatus.Pending,
                PONo = "PO1",
            };


            PurchaseOrder p2 = new PurchaseOrder()
            {
                //POID = 2,
                CreatedOn = DateTime.Now,
                ExpectedDate = DateTime.Now,
                PurchasedBy = 1,
                SupplierID = 1,
                Status = POStatus.Pending,
                PONo = "PO2",
            };

            PurchaseOrder p3 = new PurchaseOrder()
            {
                //POID = 3,
                CreatedOn = DateTime.Now,
                ExpectedDate = DateTime.Now,
                PurchasedBy = 1,
                SupplierID = 1,
                Status = POStatus.Pending,
                PONo = "PO3",
            };

            purchases.Add(p1);
            purchases.Add(p2);
            purchases.Add(p3);

            return purchases;
        }
    }
}
