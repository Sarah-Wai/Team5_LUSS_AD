using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace LUSS_API.Models
{
    public class RequestDetails
    {

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestDetailID { get; set; }
        [Required]
        public int RequestQty { get; set; }
        [Required]
        public int ItemID    { get; set; }
        [Required]
        public int RequestID { get; set; }
        public int? FullfillQty { get; set; }
       
        public int? ReceivedQty { get; set; }
        public bool isActive { get; set; }
        public virtual Request Request { get; set; }
        public virtual Item Item { get; set; }
    }
}
