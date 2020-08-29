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
        public static List<PurchaseOrder> getAllPO(List<Supplier> suppliers, List<User> users)
        {
            List<PurchaseOrder> purchases = new List<PurchaseOrder>();

            PurchaseOrder p1 = new PurchaseOrder()
            {
                CreatedOn = DateTime.Now.AddDays(-2),
                ExpectedDate = DateTime.Now.AddDays(4),
                PurchasedBy = users[7].UserID,
                SupplierID = suppliers[0].SupplierID,
                Status = POStatus.Pending,
                PONo = "PO1",
            };


            PurchaseOrder p2 = new PurchaseOrder()
            {
                CreatedOn = DateTime.Now.AddDays(-8),
                ExpectedDate = DateTime.Now.AddDays(-4),
                PurchasedBy = users[7].UserID,
                SupplierID = suppliers[4].SupplierID,
                Status = POStatus.Completed,
                ReceivedDate = DateTime.Now.AddDays(-2),
                PONo = "PO2",
            };

            PurchaseOrder p3 = new PurchaseOrder()
            {
                CreatedOn = DateTime.Now.AddDays(-3),
                ExpectedDate = DateTime.Now.AddDays(5),
                PurchasedBy = users[7].UserID,
                SupplierID = suppliers[3].SupplierID,
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
