using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Models
{
    public class CollectionPoint
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CollectionPointID { get; set; }
        [Required]
        [MaxLength(50)]
        public string PointName  { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description  { get; set; }
        [MaxLength(500)]
        public string Location   { get; set; }
}
}
