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
    public class Request
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestID   { get; set; }
        [Required]
        public EOrderStatus RequestStatus { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public int RequestPrice { get; set; }
        [Required]
        public int RequestBy { get; set; }
        [Required]
        public int ModifiedBy   { get; set; }
        [MaxLength(50)]
        public string Comment   { get; set; }
        [MaxLength(50)]
        public string RequestType { get; set; }
        [Required]
        public int ParentRequestID { get; set; }
        [Required]
        public DateTime CollectionTime { get; set; }
        public virtual User RequestedUser { get; set; }
        public virtual User ModifiedUser { get; set; }
        public virtual ICollection<RequestDetails> RequestDetails { get; set; }
    }
}
