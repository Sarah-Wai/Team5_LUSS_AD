using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Team5_LUSS.Models.AdjustmentVoucherStatus;

namespace Team5_LUSS.Models
{
    public class AdjustmentVoucher
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        public int RequestBy   { get; set; }
        [Required]
        public int ApprovedBy   { get; set; }
        [Required]
        public DateTime IssuedDate   { get; set; }
        [Required]
        [MaxLength(50)]
        public string VoucherNo   { get; set; }
        [MaxLength(500)]
        public string Comment  { get; set; }
        public string Reason { get; set; }
        [Required]
        public int ItemID { get; set; }
        public virtual User RequestUser { get; set; }
        public virtual User ApprovedUser { get; set; }
        public virtual Item Item { get; set; }
    }
}
