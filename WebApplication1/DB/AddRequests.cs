using LUSS_API.Models;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LUSS_API.Models.Status;

namespace LUSS_API.DB
{
    public class AddRequests
    {
        public static List<Request> getAllRequest(List<User> users, List<Retrieval> retrievals)
        {
            List<Request> requests = new List<Request>();

            Request r1 = new Request()
            {
                RequestStatus = EOrderStatus.Approved,
                RequestDate = DateTime.Now.AddDays(-10),
                RequestBy = users[2].UserID,
                ModifiedBy = users[0].UserID,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
            };
            Request r2 = new Request()
            {
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = users[5].UserID,
                ModifiedBy = users[5].UserID,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
            };

            Request r3 = new Request()
            {
                RequestStatus = EOrderStatus.Completed,
                RequestDate = DateTime.Now.AddDays(-6),
                RequestBy = users[4].UserID,
                ModifiedBy = users[7].UserID,
                Comment = "Test",
                RequestType = RequestType.ERequestType.Discrepancy,
                ParentRequestID = null,
                CollectionTime = DateTime.Now.AddDays(-2),
                RetrievalID = retrievals[0].RetrievalID
            };

/*            Request r4 = new Request()
            {
                RequestStatus = EOrderStatus.Received,
                RequestDate = DateTime.Now.AddDays(-2),
                RequestBy = users[4].UserID,
                ModifiedBy = users[7].UserID,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
                CollectionTime = DateTime.Now.AddDays(-1),
                RetrievalID = 2

            };
            Request r5 = new Request()
            {
                RequestStatus = EOrderStatus.PendingDelivery,
                RequestDate = DateTime.Now.AddDays(-8),
                RequestBy = users[5].UserID,
                ModifiedBy = users[7].UserID,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
                CollectionTime = DateTime.Now.AddDays(4),
                RetrievalID = 1

            };
            Request r6 = new Request()
            {

            };

            Request r7 = new Request()
            {
                RequestStatus = EOrderStatus.Rejected,
                RequestDate = DateTime.Now.AddDays(-19),
                RequestBy = users[5].UserID,
                ModifiedBy = users[0].UserID,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
            };
            Request r8 = new Request()
            {
                RequestStatus = EOrderStatus.Cancelled,
                RequestDate = DateTime.Now.AddDays(-2),
                RequestBy = users[4].UserID,
                ModifiedBy = users[4].UserID,
                Comment = "Test Cancel",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,

            };
           Request r9 = new Request()
            {
                RequestStatus = EOrderStatus.Pending,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 1,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };

            Request r10 = new Request()
            {
                RequestStatus = EOrderStatus.Rejected,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 1,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };
            Request r11 = new Request()
            {
                RequestStatus = EOrderStatus.Received,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 1,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
                CollectionTime = DateTime.Now,
                RetrievalID = 5

            };
            Request r12 = new Request()
            {
                RequestStatus = EOrderStatus.PendingDelivery,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 1,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
                CollectionTime = DateTime.Now,
                RetrievalID = 1

            };
            Request r13 = new Request()
            {
                RequestStatus = EOrderStatus.Packed,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 1,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
                CollectionTime = DateTime.Now,
                RetrievalID = 3

            };
            Request r14 = new Request()
            {
                RequestStatus = EOrderStatus.Completed,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 1,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
                CollectionTime = DateTime.Now,
                RetrievalID = 4

            };
            Request r15 = new Request()
            {
                RequestStatus = EOrderStatus.Approved,
                RequestDate = DateTime.Now,
                RequestBy = 2,
                ModifiedBy = 1,
                Comment = "Test",
                RequestType = RequestType.ERequestType.New,
                ParentRequestID = null,
                CollectionTime = DateTime.Now

            };*/
            requests.Add(r1);
            requests.Add(r2);
            requests.Add(r3);
/*            requests.Add(r4);
            requests.Add(r5);
            requests.Add(r6);
            requests.Add(r7);
            requests.Add(r8);*/
/*            requests.Add(r9);
            requests.Add(r10);
            requests.Add(r11);
            requests.Add(r12);
            requests.Add(r13);
            requests.Add(r14);
            requests.Add(r15);*/
            return requests;
        }
       


    }
}
