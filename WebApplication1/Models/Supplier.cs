using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models
{
    public class Supplier
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierID { get; set; }
        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        [Required]
        [MaxLength(500)]

        public string SupplierName { get; set; }
        [Required]
        [MaxLength(500)]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [MaxLength(50)]
        [DisplayName("Email")]
        public string email { get; set; }

      
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
