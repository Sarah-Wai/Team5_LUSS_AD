using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static LUSS_API.Models.PurchaseOrderStatus;

namespace LUSS_API.Models
{
    public class PurchaseOrder
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int POID  { get; set; }
        [Required]
        public DateTime CreatedOn  { get; set; }
        [Required]
        public DateTime ExpectedDate     { get; set; }
        [Required]
        public int PurchasedBy   { get; set; }
        [Required]
        public int SupplierID   { get; set; }
        [Required]
        public POStatus Status  { get; set; }
        //[Required]
        public DateTime? ReceivedDate  { get; set; }
        [Required]
        public string PONo { get; set; }
        //[Required]
        //public int SubTotal  { get; set; }
        public virtual ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
