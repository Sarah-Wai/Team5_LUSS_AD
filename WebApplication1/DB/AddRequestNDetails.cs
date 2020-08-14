using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LUSS_API.Models.Status;

namespace LUSS_API.DB
{
    public class AddRequestNDetails
    {
        public static List<Request> getAllRequest()
        {
            List<Request> requests = new List<Request>();

            Request r1 = new Request()
            {
                RequestID = 1,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r2 = new Request()
            {
                RequestID = 2,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r3 = new Request()
            {
                RequestID = 3,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };


            Request r4 = new Request()
            {
                RequestID = 4,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r5 = new Request()
            {
                RequestID = 5,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r6 = new Request()
            {
                RequestID = 6,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };

            Request r7 = new Request()
            {
                RequestID = 7,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r8 = new Request()
            {
                RequestID = 8,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r9 = new Request()
            {
                RequestID = 9,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };

            Request r10 = new Request()
            {
                RequestID = 10,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r11 = new Request()
            {
                RequestID = 11,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r12 = new Request()
            {
                RequestID = 12,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r13 = new Request()
            {
                RequestID = 13,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r14 = new Request()
            {
                RequestID = 14,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r15 = new Request()
            {
                RequestID = 15,
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 2,
                Comment = "Test",
                RequestType = "New",
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            requests.Add(r1);
            requests.Add(r2);
            requests.Add(r3);
            requests.Add(r4);
            requests.Add(r5);
            requests.Add(r6);
            requests.Add(r7);
            requests.Add(r8);
            requests.Add(r9);
            requests.Add(r10);
            requests.Add(r11);
            requests.Add(r12);
            requests.Add(r13);
            requests.Add(r14);
            requests.Add(r15);
            return requests;
        }
       


    }
}
