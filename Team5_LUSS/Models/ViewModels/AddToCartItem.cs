using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Models.ViewModels
{
    public class AddToCartItem
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemID { get; set; }
        [Required]
        [MaxLength(50)]
        public string ItemName { get; set; }
        public int SelectedQty { get; set; }
        [Required]
        [MaxLength(50)]
        public string UOM { get; set; }
        [Required]
        public int ReStockQty { get; set; }
        [Required]
        public int InStockQty { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        [MaxLength(50)]
        public string ItemCode { get; set; }

        [Required]
        public int ReStockLevel { get; set; }

        [Required]
        [MaxLength(500)]
        public string StoreItemLocation { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }
    }
}
