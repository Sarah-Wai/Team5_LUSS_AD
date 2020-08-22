using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Team5_LUSS.Models;

namespace Team5_LUSS.Models
{
    public class Notification
    {

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        public int FromUser { get; set; }
        public int ToUser { get; set; }

        public string RouteUri { get; set; }
        //public string Type { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsRead { get; set; }
        public virtual User User { get; set; }

    }
}
