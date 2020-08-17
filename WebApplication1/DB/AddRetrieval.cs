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

            Retrieval r1 = new Retrieval()
            {
                RetrievalID = 1,
                Status = Status.EOrderStatus.Packed,
                IssueDate = DateTime.Now,
                ModifiedBy = 1
            };

            Retrieval r2 = new Retrieval()
            {
                RetrievalID = 2,
                Status = Status.EOrderStatus.PendingDelivery,
                IssueDate = DateTime.Now,
                ModifiedBy = 1
            };

            retrievals.Add(r1);
            retrievals.Add(r2);
            return retrievals;
        }
    }
}
