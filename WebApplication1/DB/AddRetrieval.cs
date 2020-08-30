using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LUSS_API.DB
{
    public class AddRetrieval
    {
        public static List<Retrieval> getAllRetrievals()
        {
            List<Retrieval> retrievals = new List<Retrieval>();

            Retrieval rv1 = new Retrieval(); rv1.Status = Status.EOrderStatus.Completed; rv1.IssueDate = DateTime.Now.AddDays(-1); retrievals.Add(rv1);
            Retrieval rv2 = new Retrieval(); rv2.Status = Status.EOrderStatus.PendingDelivery; rv2.IssueDate = DateTime.Now.AddDays(-2); retrievals.Add(rv2);
            Retrieval rv3 = new Retrieval(); rv3.Status = Status.EOrderStatus.Approved; rv3.IssueDate = DateTime.Now.AddDays(-3); retrievals.Add(rv3);
            Retrieval rv4 = new Retrieval(); rv4.Status = Status.EOrderStatus.PendingDelivery; rv4.IssueDate = DateTime.Now.AddDays(-4); retrievals.Add(rv4);
            Retrieval rv5 = new Retrieval(); rv5.Status = Status.EOrderStatus.PendingDelivery; rv5.IssueDate = DateTime.Now.AddDays(-5); retrievals.Add(rv5);
            Retrieval rv6 = new Retrieval(); rv6.Status = Status.EOrderStatus.PendingDelivery; rv6.IssueDate = DateTime.Now.AddDays(-2); retrievals.Add(rv6);

            return retrievals;
        }
    }
}
