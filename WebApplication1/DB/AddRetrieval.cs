﻿using LUSS_API.Models;
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
                //RetrievalID = 1,
                Status = Status.EOrderStatus.Packed,
                IssueDate = DateTime.Now,
            };

            Retrieval r2 = new Retrieval()
            {
                //RetrievalID = 2,
                Status = Status.EOrderStatus.PendingDelivery,
                IssueDate = DateTime.Now,
            };

            Retrieval r3 = new Retrieval()
            {
                //RetrievalID = 3,
                Status = Status.EOrderStatus.Packed,
                IssueDate = DateTime.Now,
            };

            Retrieval r4 = new Retrieval()
            {
                //RetrievalID = 4,
                Status = Status.EOrderStatus.Completed,
                IssueDate = DateTime.Now,
            };

            Retrieval r5 = new Retrieval()
            {
                //RetrievalID = 5,
                Status = Status.EOrderStatus.Received,
                IssueDate = DateTime.Now,
            };

            retrievals.Add(r1);
            retrievals.Add(r2);
            retrievals.Add(r3);
            retrievals.Add(r4);
            retrievals.Add(r5);
            return retrievals;
        }
    }
}
