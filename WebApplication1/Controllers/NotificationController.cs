using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LUSS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<NotificationController> _logger;
        public NotificationController(ILogger<NotificationController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        ////All Users////
        [HttpGet("N_ALL/{userId}")]
        public List<Notification> GetAllActiveNotification(int userId)
        {
            List<Notification> n = context123.Notification.Where(x => x.IsRead == false && x.ToUser==userId).ToList();
            return n;
        }

        [HttpGet("N_IsRead/{notiId}")]
        public void ChangeStatus(int notiId)
        {
            if(notiId != 0)
            {
                Notification n = context123.Notification.Where(x => x.NotificationId == notiId).FirstOrDefault();
                n.IsRead = true;
                context123.SaveChanges();
            }

        }

        ////Dept Head Notifications////
        [HttpGet("N_NewRequest/{fromId}")]
        public void NewRequest(int fromId)
        {

            int reportToId = (int)context123.User.Where(x => x.UserID == fromId).Select(x=>x.ReportToID).First();
            Notification n = new Notification()
            {
                FromUser = fromId,
                ToUser = reportToId,
                RouteUri = "https://localhost:44359/StationeryRequests/StationeryRequests",
                Description = "New Request for approval",
                IsRead = false
            };
            context123.Notification.Add(n);
            context123.SaveChanges();
        }

        ////Dept Rep Notifications////
        [HttpGet("N_ReadyForCollection/{fromId}/{toId}")]
        public void ReadyForCollection(int fromId, int toId)
        {
            Notification n = new Notification()
            {
                FromUser = fromId,
                ToUser = toId,
                RouteUri = "https://localhost:44359/Collection/CollectionList",
                Description = "Stationary is ready for collection",
                IsRead = false
            };
            context123.Notification.Add(n);
            context123.SaveChanges();
        }

        ////Dept Delegate Notifications////
        [HttpGet("N_DelegateAssigned/{fromId}/{toId}")]
        public void DelegateAssigned(int fromId, int toId)
        {
            Notification n = new Notification()
            {
                FromUser = fromId,
                ToUser = toId,
                RouteUri = "https://localhost:44359/",
                Description = "You have been assigned as delegate",
                IsRead = false
            };
            context123.Notification.Add(n);
            context123.SaveChanges();
        }


        ////Adjustment Voucher Notifications////
        [HttpGet("N_ACForApproval/{fromId}/{toId}")]
        public void AdjustmentVoucherForApproval(int fromId, int toId)
        {
            Notification n = new Notification()
            {
                FromUser = fromId,
                ToUser = toId,
                RouteUri = "https://localhost:44359/",
                Description = "A new adjustment voucher pending for your approval",
                IsRead = false
            };
            context123.Notification.Add(n);
            context123.SaveChanges();
        }

        [HttpGet("N_ACApproved/{fromId}/{toId}")]
        public void AdjustmentVoucherApproved(int fromId, int toId)
        {
            Notification n = new Notification()
            {
                FromUser = fromId,
                ToUser = toId,
                RouteUri = "https://localhost:44359/",
                Description = "Your Adjustment Voucher is approved",
                IsRead = false
            };
            context123.Notification.Add(n);
            context123.SaveChanges();
        }

        [HttpGet("N_ACRejected/{fromId}/{toId}")]
        public void AdjustmentVoucherRejected(int fromId, int toId)
        {
            Notification n = new Notification()
            {
                FromUser = fromId,
                ToUser = toId,
                RouteUri = "https://localhost:44359/",
                Description = "Your Adjustment Voucher is rejected",
                IsRead = false
            };
            context123.Notification.Add(n);
            context123.SaveChanges();
        }


        ////Employee Notifications////
        [HttpGet("N_RqApproved/{fromId}/{toId}")]
        public void RequestApproved(int fromId, int toId)
        {
            Notification n = new Notification()
            {
                FromUser = fromId,
                ToUser = toId,
                RouteUri = "https://localhost:44359/",
                Description = "Your Request is approved",
                IsRead = false
            };
            context123.Notification.Add(n);
            context123.SaveChanges();
        }
        [HttpGet("N_RqRejected/{fromId}/{toId}")]
        public void RequestRejected(int fromId, int toId)
        {
            Notification n = new Notification()
            {
                FromUser = fromId,
                ToUser = toId,
                RouteUri = "https://localhost:44359/",
                Description = "Your Request is rejected",
                IsRead = false
            };
            context123.Notification.Add(n);
            context123.SaveChanges();
        }
    }
}
