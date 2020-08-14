using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Models
{
    public class PurchaseOrder
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        public string Status  { get; set; }
        [Required]
        public DateTime ReceivedDate  { get; set; }
        //[Required]
        //public string PONo { get; set; }
        //[Required]
        //public int SubTotal  { get; set; }
        //public virtual ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; }
    }
}
