using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models
{
    public class Notification
    {

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int NotificationId { get; set; }
        [Required]
        public int FromUser { get; set; }
        [Required]
        public int ToUser { get; set; }
        [Required]
        public string RouteUri { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsRead { get; set; }
        public virtual User User { get; set; }

    }
}
