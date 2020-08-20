using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static LUSS_API.Models.Status;

namespace LUSS_API.Models
{
    public class Retrieval
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RetrievalID { get; set; }
        [Required]
        public EOrderStatus Status { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        //[Required]
        //public int RequestID { get; set; }
        //[Required]
        //public int ModifiedBy { get; set; }

        //public virtual ICollection<RequestDetails> RequestDetails { get; set; }
        //public virtual User User { get; set; }
    }
}
