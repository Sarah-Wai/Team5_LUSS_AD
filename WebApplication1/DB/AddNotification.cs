using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.DB
{
    public class AddNotification
    {
        public static List<Notification> getAllNotification(List<User> users)
        {
            List<Notification> notifications = new List<Notification>();
            string api = "http://localhost:44359";

            Notification n1 = new Notification()
            {
                ToUser = users[0].UserID,
                Description = "New Request for approval",
                IsRead = false,
                RouteUri = api + "/StationeryRequests/StationeryRequests"
            };

            Notification n2 = new Notification()
            {
                ToUser = users[2].UserID,
                Description = "Stationary is ready for collection now.",
                IsRead = false,
                RouteUri = api + "/Collection/collectionList"
            };

            Notification n3 = new Notification()
            {
                ToUser = users[8].UserID,
                Description = "Your Adjustment Voucher is approved",
                IsRead = false,
                RouteUri = api + "/AdjustmentList/AdjustmentVouchers"
            };

            notifications.Add(n1);
            notifications.Add(n2);
            notifications.Add(n3);

            return notifications;
        }


    }
}
