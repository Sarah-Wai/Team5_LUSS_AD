using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.Models.ViewModels
{
    public class ItemRequest
    {
        public int UserID { get; set; }
        public virtual List<AddToCartItem> ItemList { get; set; }
    }
}
