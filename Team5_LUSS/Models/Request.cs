using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Team5_LUSS.Models.RequestType;
using static Team5_LUSS.Models.Status;

namespace Team5_LUSS.Models
{
    public class Request
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestID { get; set; }
        [Required]
        public EOrderStatus RequestStatus { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public int RequestBy { get; set; }
        [Required]
        public int ModifiedBy { get; set; }
        [MaxLength(50)]
        public string Comment { get; set; }
        [MaxLength(50)]
        public ERequestType RequestType { get; set; }
        public int? ParentRequestID { get; set; }
        [Required]
        public DateTime CollectionTime { get; set; }
        public virtual User ModifiedByUser { get; set; }
        public virtual User RequestByUser { get; set; }
        public virtual ICollection<RequestDetails> RequestDetails { get; set; }
    }
}
