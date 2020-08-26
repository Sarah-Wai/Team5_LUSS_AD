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
                RequestID = 2,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del3 = new RequestDetails()
            {
               // RequestDetailID = 3,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 3,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del4 = new RequestDetails()
            {
               // RequestDetailID = 4,
                RequestQty = 40,
                ItemID = 1,
                RequestID = 4,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del5 = new RequestDetails()
            {
               // RequestDetailID = 5,
                RequestQty = 50,
                ItemID = 2,
                RequestID = 5,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del6 = new RequestDetails()
            {
               // RequestDetailID = 6,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 6,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del7 = new RequestDetails()
            {
                // RequestDetailID = 7,
                RequestQty = 30,
                ItemID = 12,
                RequestID = 7,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del8 = new RequestDetails()
            {
                // RequestDetailID = 8,
                RequestQty = 30,
                ItemID = 4,
                RequestID = 8,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del9 = new RequestDetails()
            {
                // RequestDetailID = 9,
                RequestQty = 30,
                ItemID = 11,
                RequestID = 9,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del10 = new RequestDetails()
            {
                // RequestDetailID = 10,
                RequestQty = 30,
                ItemID = 8,
                RequestID = 10,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del11 = new RequestDetails()
            {
                // RequestDetailID = 11,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 11,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del12 = new RequestDetails()
            {
                // RequestDetailID = 12,
                RequestQty = 30,
                ItemID = 9,
                RequestID = 12,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del13 = new RequestDetails()
            {
                // RequestDetailID = 13,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 13,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del14 = new RequestDetails()
            {
                // RequestDetailID = 14,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 14,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del15 = new RequestDetails()
            {
                // RequestDetailID = 15,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 15,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del16 = new RequestDetails()
            {
                // RequestDetailID = 16,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 1,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del17 = new RequestDetails()
            {
                // RequestDetailID = 17,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 12,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del18 = new RequestDetails()
            {
                // RequestDetailID = 18,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 2,
                FullfillQty = null,
                ReceivedQty = null,
                isActive = true
            };
            RequestDetails del19 = new RequestDetails()
            {
                // RequestDetailID = 19,
                RequestQty = 30,
                ItemID = 3,
                RequestID = 2,
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
            requestDetails.Add(del7);
            requestDetails.Add(del8);
            requestDetails.Add(del9);
            requestDetails.Add(del10);
            requestDetails.Add(del11);
            requestDetails.Add(del12);
            requestDetails.Add(del13);
            requestDetails.Add(del14);
            requestDetails.Add(del15);
            requestDetails.Add(del16);
            requestDetails.Add(del17);
            requestDetails.Add(del18);
            return requestDetails;
        }
    }
}
