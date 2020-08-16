using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.Models;

namespace LUSS_API.DB
{
    public class AddItemPrice
    {
        public static List<ItemPrice> getAllItemPrice()
        {
            List<ItemPrice> itemPrices = new List<ItemPrice>();

            ItemPrice itemPrice1 = new ItemPrice ()
            {
                ItemPriceID = 1,
                ItemID = 1,
                SupplierID = 1,
                Price = 2,
            };

            ItemPrice itemPrice2 = new ItemPrice()
            {
                ItemPriceID = 2,
                ItemID = 1,
                SupplierID = 2,
                Price = 2,
            };

            ItemPrice itemPrice3 = new ItemPrice()
            {
                ItemPriceID = 3,
                ItemID = 1,
                SupplierID = 3,
                Price = 2,
            };

            itemPrices.Add(itemPrice1);
            itemPrices.Add(itemPrice2);
            itemPrices.Add(itemPrice3);

            return itemPrices;
        }
    }
}
