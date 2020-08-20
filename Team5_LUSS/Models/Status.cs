using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Models
{
    public class Status
    {
        public enum EOrderStatus { Pending, Approved, Rejected, Packed, PendingDelivery, Received, Completed , Cancelled}
    }
}
