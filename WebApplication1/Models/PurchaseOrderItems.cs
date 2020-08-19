using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models
{
    public class PurchaseOrderItems
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int POItemID  { get; set; }
        [Required]
        public int POID  { get; set; }
        [Required]
        public int ItemID   { get; set; }
        [Required]
        public int OrderQty   { get; set; }
        //[Required]
        //public int Amount   { get; set; }
        //[Required]
        public int ReceivedQty   { get; set; }
        //[Required]
        //public int Price   { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Item Item { get; set; }
    }
}
