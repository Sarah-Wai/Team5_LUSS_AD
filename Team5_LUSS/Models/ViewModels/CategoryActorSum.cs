using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Models.ViewModels
{
    public class CategoryActorSum
    {
        
        public string Category { get; set; }
        public string Actor { get; set; }
        public int? Sum { get; set; }




    }
}
