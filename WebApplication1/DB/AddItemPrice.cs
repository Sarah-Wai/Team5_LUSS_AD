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

            ItemPrice itemPrice4 = new ItemPrice()
            {
                ItemPriceID = 4,
                ItemID = 2,
                SupplierID = 1,
                Price = 5,
            };

            ItemPrice itemPrice5 = new ItemPrice()
            {
                ItemPriceID = 5,
                ItemID = 2,
                SupplierID = 2,
                Price = 6,
            };

            ItemPrice itemPrice6 = new ItemPrice()
            {
                ItemPriceID = 6,
                ItemID = 2,
                SupplierID = 3,
                Price = 7,
            };


            itemPrices.Add(itemPrice1);
            itemPrices.Add(itemPrice2);
            itemPrices.Add(itemPrice3);
            itemPrices.Add(itemPrice4);
            itemPrices.Add(itemPrice5);
            itemPrices.Add(itemPrice6);

            return itemPrices;
        }
    }
}
