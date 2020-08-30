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
            Retrieval rv2 = new Retrieval(); rv1.Status = Status.EOrderStatus.PendingDelivery; rv1.IssueDate = DateTime.Now.AddDays(-2); retrievals.Add(rv2);
            Retrieval rv3 = new Retrieval(); rv1.Status = Status.EOrderStatus.Approved; rv1.IssueDate = DateTime.Now.AddDays(-3); retrievals.Add(rv3);
            Retrieval rv4 = new Retrieval(); rv1.Status = Status.EOrderStatus.PendingDelivery; rv1.IssueDate = DateTime.Now.AddDays(-4); retrievals.Add(rv4);
            Retrieval rv5 = new Retrieval(); rv1.Status = Status.EOrderStatus.PendingDelivery; rv1.IssueDate = DateTime.Now.AddDays(-5); retrievals.Add(rv5);

            return retrievals;
        }
    }
}
