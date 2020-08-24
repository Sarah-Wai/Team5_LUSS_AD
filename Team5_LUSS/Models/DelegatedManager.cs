using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Models
{
    public class DelegatedManager
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DelegatedManagerID   { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}")]
        public DateTime FromDate   { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}")]
        public DateTime ToDate   { get; set; }
        [Required]
        public bool isActive { get; set; }
        [Required]
        public int UserID    { get; set; } 
        public virtual User User { get; set; }

    }
}
