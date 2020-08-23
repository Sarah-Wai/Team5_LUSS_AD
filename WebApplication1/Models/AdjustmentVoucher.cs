using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static LUSS_API.Models.AdjustmentVoucherStatus;

namespace LUSS_API.Models
{
    public class AdjustmentVoucher
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdjustmentID   { get; set; }
        [Required]
        public int AdjustQty { get; set; }
        [Required]
        [MaxLength(50)]
        public string AdjustType  { get; set; }
        [Required]
        [MaxLength(50)]
        public AdjustmentStatus Status   { get; set; }
        [Required]
        public int TotalCost   { get; set; }
      
        [Required]
        public DateTime IssuedDate   { get; set; }
        [Required]
        [MaxLength(50)]
        public string VoucherNo   { get; set; }
        [MaxLength(500)]
        public string Comment  { get; set; }
        public string? Reason { get; set; }
        [Required]
        public int ItemID { get; set; }

        public int? ApprovedByID { get; set; }

        public int? RequestByID { get; set; }

        //[ForeignKey("RequestByID")]
        public virtual User RequestedByUser { get; set; }

        //[ForeignKey("ApprovedByID")]
        public virtual User ApprovedByUser { get; set; }
        public virtual Item Item { get; set; }
    }
}
