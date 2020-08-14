using Team5_LUSS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Models
{
    public class Department
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepartmentID { get; set; }
        [Required]
        [MaxLength(500)]
        public string DepartmentName { get; set; }
        [MaxLength(50)]
        public string PhoneNo { get; set; }
        [MaxLength(50)]
        public string Fax { get; set; }
        [Required]
        public int CollectionPointID { get; set; }
        [Required]
        [MaxLength(50)]
        public string DepartmentCode { get; set; }
       public virtual CollectionPoint CollectionPoint { get; set; }
    }
}
