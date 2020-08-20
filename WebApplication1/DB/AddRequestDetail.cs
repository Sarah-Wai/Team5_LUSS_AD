using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.DB
{
    public class AddRequestDetail
    {
        public static List<RequestDetails> getAllRequestDetails()
        {
            List<RequestDetails> requestDetails = new List<RequestDetails>();
            RequestDetails del1 = new RequestDetails()
            {
                // RequestDetailID = 1,
                RequestQty = 10,
                ItemID = 1,
                RequestID = 1,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del2 = new RequestDetails()
            {
               // RequestDetailID = 2,
                RequestQty = 20,
                ItemID = 2,
                RequestID = 1,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del3 = new RequestDetails()
            {
               // RequestDetailID = 3,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 1,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del4 = new RequestDetails()
            {
               // RequestDetailID = 4,
                RequestQty = 40,
                ItemID = 1,
                RequestID = 2,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del5 = new RequestDetails()
            {
               // RequestDetailID = 5,
                RequestQty = 50,
                ItemID = 2,
                RequestID = 2,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del6 = new RequestDetails()
            {
               // RequestDetailID = 6,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 3,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            requestDetails.Add(del1);
            requestDetails.Add(del2);
            requestDetails.Add(del3);
            requestDetails.Add(del4);
            requestDetails.Add(del5);
            requestDetails.Add(del6);
            return requestDetails;
        }
    }
}
