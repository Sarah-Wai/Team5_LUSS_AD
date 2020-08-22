using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Models.ViewModels
{
   public class ItemRequest
    {
        public int UserID { get; set; }
        public virtual List<AddToCartItem> ItemList { get; set; }
    }
}
