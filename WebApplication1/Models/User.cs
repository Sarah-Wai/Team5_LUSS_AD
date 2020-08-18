using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models
{
    public class User
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int UserID { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName  { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Designation { get; set; }
        [MaxLength(50)]
        public string  ContactNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email{ get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        [Required]
        public bool IsRepresentative { get; set; }
      
        public int? ReportToID   { get; set; }
        [Required]
        public int DepartmentID   { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Request> RequestMade { get; set; }

        public virtual ICollection<Request> RequestModified { get; set; }

        public virtual ICollection<AdjustmentVoucher> RequestedBy { get; set; }
        public virtual ICollection<AdjustmentVoucher> ApprovedBy { get; set; }
        public virtual DelegatedManager DelegatedManager { get; set; }


    }
}
