using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Models
{
    public class Cart
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 


        public int CartID { get; set; }
        [Required]
        public int UserID   { get; set; }
        [Required]
        public int ItemID   { get; set; }
        [Required]
        public int Qty   { get; set; }
        public virtual Item Item{ get; set; }
    }
}
