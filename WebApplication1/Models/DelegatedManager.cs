using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models
{
    public class DelegatedManager
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DelegatedManagerID   { get; set; }
        [Required]
        public DateTime FromDate   { get; set; }
        [Required]
        public DateTime ToDate   { get; set; }
        [Required]
        public int UserID    { get; set; } 
        public virtual User User { get; set; }

    }
}
