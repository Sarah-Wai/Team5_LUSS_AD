using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.DB
{
    public class AddRequestDetail
    {
        public static List<RequestDetails> getAllRequestDetails(List<Item> items, List<Request> requests)
        {
            List<RequestDetails> requestDetails = new List<RequestDetails>();
            RequestDetails del1 = new RequestDetails()
            {
                RequestQty = 10,
                ItemID = items[4].ItemID,
                RequestID = requests[2].RequestID,
                FullfillQty = 8,
                ReceivedQty = 8,

            };
            RequestDetails del2 = new RequestDetails()
            {
                RequestQty = 12,
                ItemID = items[7].ItemID,
                RequestID = requests[2].RequestID,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del3 = new RequestDetails()
            {
                RequestQty = 1,
                ItemID = items[34].ItemID,
                RequestID = requests[2].RequestID,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del4 = new RequestDetails()
            {
                RequestQty = 13,
                ItemID = items[7].ItemID,
                RequestID = requests[1].RequestID,
                FullfillQty = null,
                ReceivedQty = null,
            };
            RequestDetails del5 = new RequestDetails()
            {
                RequestQty = 6,
                ItemID = items[9].ItemID,
                RequestID = requests[1].RequestID,
                FullfillQty = null,
                ReceivedQty = null,

            };
           RequestDetails del6 = new RequestDetails()
            {

               RequestQty = 6,
               ItemID = items[44].ItemID,
               RequestID = requests[0].RequestID,
               FullfillQty = 6,
               ReceivedQty = 6,
           };
            RequestDetails del7 = new RequestDetails()
            {

                RequestQty = 16,
                ItemID = items[14].ItemID,
                RequestID = requests[0].RequestID,
                FullfillQty = 16,
                ReceivedQty = 16,

            };
 /*           RequestDetails del8 = new RequestDetails()
            {

                RequestQty = 30,
                ItemID = 4,
                RequestID = 8,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del9 = new RequestDetails()
            {

                RequestQty = 30,
                ItemID = 11,
                RequestID = 9,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del10 = new RequestDetails()
            {
                RequestQty = 30,
                ItemID = 8,
                RequestID = 10,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del11 = new RequestDetails()
            {

                RequestQty = 30,
                ItemID = 3,
                RequestID = 11,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del12 = new RequestDetails()
            {
                RequestQty = 30,
                ItemID = 9,
                RequestID = 12,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del13 = new RequestDetails()
            {

                RequestQty = 30,
                ItemID = 3,
                RequestID = 13,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del14 = new RequestDetails()
            {

                RequestQty = 30,
                ItemID = 3,
                RequestID = 14,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del15 = new RequestDetails()
            {

                RequestQty = 30,
                ItemID = 3,
                RequestID = 15,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del16 = new RequestDetails()
            {

                RequestQty = 30,
                ItemID = 3,
                RequestID = 1,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del17 = new RequestDetails()
            {

                RequestQty = 30,
                ItemID = 3,
                RequestID = 12,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del18 = new RequestDetails()
            {

                RequestQty = 30,
                ItemID = 3,
                RequestID = 2,
                FullfillQty = null,
                ReceivedQty = null,

            };
            RequestDetails del19 = new RequestDetails()
            {
                RequestQty = 30,
                ItemID = 3,
                RequestID = 2,
                FullfillQty = null,
                ReceivedQty = null,

            };*/

            requestDetails.Add(del1);
            requestDetails.Add(del2);
            requestDetails.Add(del3);
            requestDetails.Add(del4);
            requestDetails.Add(del5);
            requestDetails.Add(del6);
            requestDetails.Add(del7);
 /*           requestDetails.Add(del8);
            requestDetails.Add(del9);
            requestDetails.Add(del10);
            requestDetails.Add(del11);
            requestDetails.Add(del12);
            requestDetails.Add(del13);
            requestDetails.Add(del14);
            requestDetails.Add(del15);
            requestDetails.Add(del16);
            requestDetails.Add(del17);
            requestDetails.Add(del18);*/
            return requestDetails;
        }
    }
}
