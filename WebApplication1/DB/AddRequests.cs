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

            //history data = completed requests
            Request r1 = new Request(); r1.RequestStatus = EOrderStatus.Completed; r1.RequestDate = DateTime.Now.AddDays(-30); r1.RequestBy = users[4].UserID; r1.ModifiedBy = users[8].UserID; r1.Comment = "ok"; r1.RequestType = 0; r1.ParentRequestID = null; r1.CollectionTime = DateTime.Now.AddDays(-27); r1.RetrievalID = retrievals[0].RetrievalID; requests.Add(r1);
            Request r2 = new Request(); r2.RequestStatus = EOrderStatus.Completed; r2.RequestDate = DateTime.Now.AddDays(-60); r2.RequestBy = users[4].UserID; r2.ModifiedBy = users[8].UserID; r2.Comment = "ok"; r2.RequestType = 0; r2.ParentRequestID = null; r2.CollectionTime = DateTime.Now.AddDays(-57); r2.RetrievalID = retrievals[0].RetrievalID; requests.Add(r2);
            Request r3 = new Request(); r3.RequestStatus = EOrderStatus.Completed; r3.RequestDate = DateTime.Now.AddDays(-90); r3.RequestBy = users[4].UserID; r3.ModifiedBy = users[8].UserID; r3.Comment = "ok"; r3.RequestType = 0; r3.ParentRequestID = null; r3.CollectionTime = DateTime.Now.AddDays(-87); r3.RetrievalID = retrievals[0].RetrievalID; requests.Add(r3);
            Request r4 = new Request(); r4.RequestStatus = EOrderStatus.Completed; r4.RequestDate = DateTime.Now.AddDays(-120); r4.RequestBy = users[2].UserID; r4.ModifiedBy = users[8].UserID; r4.Comment = "ok"; r4.RequestType = 0; r4.ParentRequestID = null; r4.CollectionTime = DateTime.Now.AddDays(-117); r4.RetrievalID = retrievals[0].RetrievalID; requests.Add(r4);
            Request r5 = new Request(); r5.RequestStatus = EOrderStatus.Completed; r5.RequestDate = DateTime.Now.AddDays(-150); r5.RequestBy = users[4].UserID; r5.ModifiedBy = users[8].UserID; r5.Comment = "ok"; r5.RequestType = 0; r5.ParentRequestID = null; r5.CollectionTime = DateTime.Now.AddDays(-147); r5.RetrievalID = retrievals[0].RetrievalID; requests.Add(r5);
            Request r6 = new Request(); r6.RequestStatus = EOrderStatus.Completed; r6.RequestDate = DateTime.Now.AddDays(-180); r6.RequestBy = users[4].UserID; r6.ModifiedBy = users[8].UserID; r6.Comment = "ok"; r6.RequestType = 0; r6.ParentRequestID = null; r6.CollectionTime = DateTime.Now.AddDays(-177); r6.RetrievalID = retrievals[0].RetrievalID; requests.Add(r6);
            Request r7 = new Request(); r7.RequestStatus = EOrderStatus.Completed; r7.RequestDate = DateTime.Now.AddDays(-30); r7.RequestBy = users[2].UserID; r7.ModifiedBy = users[8].UserID; r7.Comment = "ok"; r7.RequestType = 0; r7.ParentRequestID = null; r7.CollectionTime = DateTime.Now.AddDays(-27); r7.RetrievalID = retrievals[0].RetrievalID; requests.Add(r7);
            Request r8 = new Request(); r8.RequestStatus = EOrderStatus.Completed; r8.RequestDate = DateTime.Now.AddDays(-60); r8.RequestBy = users[2].UserID; r8.ModifiedBy = users[8].UserID; r8.Comment = "ok"; r8.RequestType = 0; r8.ParentRequestID = null; r8.CollectionTime = DateTime.Now.AddDays(-57); r8.RetrievalID = retrievals[0].RetrievalID; requests.Add(r8);
            Request r9 = new Request(); r9.RequestStatus = EOrderStatus.Completed; r9.RequestDate = DateTime.Now.AddDays(-90); r9.RequestBy = users[2].UserID; r9.ModifiedBy = users[8].UserID; r9.Comment = "ok"; r9.RequestType = 0; r9.ParentRequestID = null; r9.CollectionTime = DateTime.Now.AddDays(-87); r9.RetrievalID = retrievals[0].RetrievalID; requests.Add(r9);
            Request r10 = new Request(); r10.RequestStatus = EOrderStatus.Completed; r10.RequestDate = DateTime.Now.AddDays(-120); r10.RequestBy = users[15].UserID; r10.ModifiedBy = users[8].UserID; r10.Comment = "ok"; r10.RequestType = 0; r10.ParentRequestID = null; r10.CollectionTime = DateTime.Now.AddDays(-117); r10.RetrievalID = retrievals[0].RetrievalID; requests.Add(r10);
            Request r11 = new Request(); r11.RequestStatus = EOrderStatus.Completed; r11.RequestDate = DateTime.Now.AddDays(-150); r11.RequestBy = users[2].UserID; r11.ModifiedBy = users[8].UserID; r11.Comment = "ok"; r11.RequestType = 0; r11.ParentRequestID = null; r11.CollectionTime = DateTime.Now.AddDays(-147); r11.RetrievalID = retrievals[0].RetrievalID; requests.Add(r11);
            Request r12 = new Request(); r12.RequestStatus = EOrderStatus.Completed; r12.RequestDate = DateTime.Now.AddDays(-180); r12.RequestBy = users[15].UserID; r12.ModifiedBy = users[8].UserID; r12.Comment = "ok"; r12.RequestType = 0; r12.ParentRequestID = null; r12.CollectionTime = DateTime.Now.AddDays(-177); r12.RetrievalID = retrievals[0].RetrievalID; requests.Add(r12);
            Request r13 = new Request(); r13.RequestStatus = EOrderStatus.Completed; r13.RequestDate = DateTime.Now.AddDays(-30); r13.RequestBy = users[15].UserID; r13.ModifiedBy = users[8].UserID; r13.Comment = "ok"; r13.RequestType = 0; r13.ParentRequestID = null; r13.CollectionTime = DateTime.Now.AddDays(-27); r13.RetrievalID = retrievals[0].RetrievalID; requests.Add(r13);
            Request r14 = new Request(); r14.RequestStatus = EOrderStatus.Completed; r14.RequestDate = DateTime.Now.AddDays(-60); r14.RequestBy = users[13].UserID; r14.ModifiedBy = users[8].UserID; r14.Comment = "ok"; r14.RequestType = 0; r14.ParentRequestID = null; r14.CollectionTime = DateTime.Now.AddDays(-57); r14.RetrievalID = retrievals[0].RetrievalID; requests.Add(r14);
            Request r15 = new Request(); r15.RequestStatus = EOrderStatus.Completed; r15.RequestDate = DateTime.Now.AddDays(-90); r15.RequestBy = users[15].UserID; r15.ModifiedBy = users[8].UserID; r15.Comment = "ok"; r15.RequestType = 0; r15.ParentRequestID = null; r15.CollectionTime = DateTime.Now.AddDays(-87); r15.RetrievalID = retrievals[0].RetrievalID; requests.Add(r15);
            Request r16 = new Request(); r16.RequestStatus = EOrderStatus.Completed; r16.RequestDate = DateTime.Now.AddDays(-120); r16.RequestBy = users[13].UserID; r16.ModifiedBy = users[8].UserID; r16.Comment = "ok"; r16.RequestType = 0; r16.ParentRequestID = null; r16.CollectionTime = DateTime.Now.AddDays(-117); r16.RetrievalID = retrievals[0].RetrievalID; requests.Add(r16);
            Request r17 = new Request(); r17.RequestStatus = EOrderStatus.Completed; r17.RequestDate = DateTime.Now.AddDays(-30); r17.RequestBy = users[13].UserID; r17.ModifiedBy = users[8].UserID; r17.Comment = "ok"; r17.RequestType = 0; r17.ParentRequestID = null; r17.CollectionTime = DateTime.Now.AddDays(-27); r17.RetrievalID = retrievals[0].RetrievalID; requests.Add(r17);
            Request r18 = new Request(); r18.RequestStatus = EOrderStatus.Completed; r18.RequestDate = DateTime.Now.AddDays(-60); r18.RequestBy = users[13].UserID; r18.ModifiedBy = users[8].UserID; r18.Comment = "ok"; r18.RequestType = 0; r18.ParentRequestID = null; r18.CollectionTime = DateTime.Now.AddDays(-57); r18.RetrievalID = retrievals[0].RetrievalID; requests.Add(r18);

            //new requests
            Request r19 = new Request(); r19.RequestStatus = EOrderStatus.Pending; r19.RequestDate = DateTime.Now.AddDays(-1); r19.RequestBy = users[2].UserID; r19.ModifiedBy = users[2].UserID; r19.Comment = "ok"; r19.RequestType = 0; r19.ParentRequestID = null; r19.CollectionTime = DateTime.Now.AddDays(-2); r19.RetrievalID = null; requests.Add(r19);
            Request r20 = new Request(); r20.RequestStatus = EOrderStatus.Rejected; r20.RequestDate = DateTime.Now.AddDays(-2); r20.RequestBy = users[2].UserID; r20.ModifiedBy = users[2].UserID; r20.Comment = "ok"; r20.RequestType = 0; r20.ParentRequestID = null; r20.CollectionTime = DateTime.Now.AddDays(-1); r20.RetrievalID = null; requests.Add(r20);
            Request r21 = new Request(); r21.RequestStatus = EOrderStatus.Received; r21.RequestDate = DateTime.Now.AddDays(-3); r21.RequestBy = users[2].UserID; r21.ModifiedBy = users[8].UserID; r21.Comment = "ok"; r21.RequestType = 0; r21.ParentRequestID = null; r21.CollectionTime = DateTime.Now.AddDays(0); r21.RetrievalID = 4; requests.Add(r21);
            Request r22 = new Request(); r22.RequestStatus = EOrderStatus.PendingDelivery; r22.RequestDate = DateTime.Now.AddDays(-4); r22.RequestBy = users[4].UserID; r22.ModifiedBy = users[8].UserID; r22.Comment = "ok"; r22.RequestType = 0; r22.ParentRequestID = null; r22.CollectionTime = DateTime.Now.AddDays(3); r22.RetrievalID = 2; requests.Add(r22);
            Request r23 = new Request(); r23.RequestStatus = EOrderStatus.Received; r23.RequestDate = DateTime.Now.AddDays(-5); r23.RequestBy = users[2].UserID; r23.ModifiedBy = users[8].UserID; r23.Comment = "ok"; r23.RequestType = 0; r23.ParentRequestID = null; r23.CollectionTime = DateTime.Now.AddDays(-2); r23.RetrievalID = 5; requests.Add(r23);
            Request r24 = new Request(); r24.RequestStatus = EOrderStatus.Approved; r24.RequestDate = DateTime.Now.AddDays(-6); r24.RequestBy = users[4].UserID; r24.ModifiedBy = users[4].UserID; r24.Comment = "ok"; r24.RequestType = 0; r24.ParentRequestID = null; r24.CollectionTime = DateTime.Now.AddDays(4); r24.RetrievalID = 3; requests.Add(r24);
            Request r25 = new Request(); r25.RequestStatus = EOrderStatus.Approved; r25.RequestDate = DateTime.Now.AddDays(-7); r25.RequestBy = users[2].UserID; r25.ModifiedBy = users[2].UserID; r25.Comment = "ok"; r25.RequestType = 0; r25.ParentRequestID = null; r25.CollectionTime = DateTime.Now.AddDays(4); r25.RetrievalID = 3; requests.Add(r25);
            //new
            Request r26 = new Request(); r26.RequestStatus = EOrderStatus.PendingDelivery; r26.RequestDate = DateTime.Now.AddDays(-8); r26.RequestBy = users[4].UserID; r26.ModifiedBy = users[8].UserID; r26.Comment = "ok"; r26.RequestType = 0; r26.ParentRequestID = null; r26.CollectionTime = DateTime.Now.AddDays(4); r26.RetrievalID = 3; requests.Add(r26);
            Request r27 = new Request(); r27.RequestStatus = EOrderStatus.PendingDelivery; r27.RequestDate = DateTime.Now.AddDays(-6); r27.RequestBy = users[4].UserID; r27.ModifiedBy = users[8].UserID; r27.Comment = "ok"; r27.RequestType = 0; r27.ParentRequestID = null; r27.CollectionTime = DateTime.Now.AddDays(4); r27.RetrievalID = 3; requests.Add(r27);




            return requests;
        }
       


    }
}
