using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models
{
    public class ItemPrice
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemPriceID { get; set; }
        [Required]
        public int ItemID   { get; set; }
        [Required]
        public int SupplierID   { get; set; }
        [Required]
        public int Price   { get; set; }
        public virtual Item Item { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
