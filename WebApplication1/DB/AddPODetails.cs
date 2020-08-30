using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.DB
{
    public class AddPODetails
    {

        public static List<PurchaseOrderItems> getAllPOItems(List<PurchaseOrder> pos, List<Item> items)
        {
            List<PurchaseOrderItems> poItems = new List<PurchaseOrderItems>();

            /*PurchaseOrderItems p1 = new PurchaseOrderItems()
            {
                
                POID = pos[0].POID,
                ItemID = items[0].ItemID,
                OrderQty = 10,

            };
            PurchaseOrderItems p2 = new PurchaseOrderItems()
            {

                POID = pos[0].POID,
                ItemID = items[4].ItemID,
                OrderQty = 108,
            };
            PurchaseOrderItems p3 = new PurchaseOrderItems()
            {

                POID = pos[1].POID,
                ItemID = items[66].ItemID,
                OrderQty = 8,
                ReceivedQty = 8
            };
            PurchaseOrderItems p4 = new PurchaseOrderItems()
            {

                POID = pos[2].POID,
                ItemID = items[55].ItemID,
                OrderQty = 87,
            };
            PurchaseOrderItems p5 = new PurchaseOrderItems()
            {

                POID = pos[2].POID,
                ItemID = items[13].ItemID,
                OrderQty = 17,
            };*/

            //history data
            PurchaseOrderItems p1 = new PurchaseOrderItems(); p1.POID = 1; p1.ItemID = 14; p1.OrderQty = 17; p1.ReceivedQty = 17; poItems.Add(p1);
            PurchaseOrderItems p2 = new PurchaseOrderItems(); p2.POID = 1; p2.ItemID = 56; p2.OrderQty = 87; p2.ReceivedQty = 87; poItems.Add(p2);
            PurchaseOrderItems p3 = new PurchaseOrderItems(); p3.POID = 2; p3.ItemID = 67; p3.OrderQty = 8; p3.ReceivedQty = 8; poItems.Add(p3);
            PurchaseOrderItems p4 = new PurchaseOrderItems(); p4.POID = 3; p4.ItemID = 5; p4.OrderQty = 108; p4.ReceivedQty = 108; poItems.Add(p4);
            PurchaseOrderItems p5 = new PurchaseOrderItems(); p5.POID = 3; p5.ItemID = 1; p5.OrderQty = 10; p5.ReceivedQty = 10; poItems.Add(p5);
            PurchaseOrderItems p6 = new PurchaseOrderItems(); p6.POID = 4; p6.ItemID = 2; p6.OrderQty = 30; p6.ReceivedQty = 30; poItems.Add(p6);
            PurchaseOrderItems p7 = new PurchaseOrderItems(); p7.POID = 5; p7.ItemID = 4; p7.OrderQty = 30; p7.ReceivedQty = 30; poItems.Add(p7);
            PurchaseOrderItems p8 = new PurchaseOrderItems(); p8.POID = 6; p8.ItemID = 61; p8.OrderQty = 50; p8.ReceivedQty = 50; poItems.Add(p8);
            PurchaseOrderItems p9 = new PurchaseOrderItems(); p9.POID = 7; p9.ItemID = 6; p9.OrderQty = 30; p9.ReceivedQty = 30; poItems.Add(p9);
            PurchaseOrderItems p10 = new PurchaseOrderItems(); p10.POID = 8; p10.ItemID = 8; p10.OrderQty = 400; p10.ReceivedQty = 400; poItems.Add(p10);
            PurchaseOrderItems p11 = new PurchaseOrderItems(); p11.POID = 9; p11.ItemID = 10; p11.OrderQty = 400; p11.ReceivedQty = 400; poItems.Add(p11);
            PurchaseOrderItems p12 = new PurchaseOrderItems(); p12.POID = 10; p12.ItemID = 16; p12.OrderQty = 20; p12.ReceivedQty = 20; poItems.Add(p12);
            PurchaseOrderItems p13 = new PurchaseOrderItems(); p13.POID = 11; p13.ItemID = 17; p13.OrderQty = 50; p13.ReceivedQty = 50; poItems.Add(p13);
            PurchaseOrderItems p14 = new PurchaseOrderItems(); p14.POID = 12; p14.ItemID = 23; p14.OrderQty = 50; p14.ReceivedQty = 50; poItems.Add(p14);
            PurchaseOrderItems p15 = new PurchaseOrderItems(); p15.POID = 13; p15.ItemID = 28; p15.OrderQty = 150; p15.ReceivedQty = 150; poItems.Add(p15);
            PurchaseOrderItems p16 = new PurchaseOrderItems(); p16.POID = 14; p16.ItemID = 30; p16.OrderQty = 150; p16.ReceivedQty = 150; poItems.Add(p16);
            PurchaseOrderItems p17 = new PurchaseOrderItems(); p17.POID = 15; p17.ItemID = 33; p17.OrderQty = 150; p17.ReceivedQty = 150; poItems.Add(p17);
            PurchaseOrderItems p18 = new PurchaseOrderItems(); p18.POID = 16; p18.ItemID = 38; p18.OrderQty = 20; p18.ReceivedQty = 20; poItems.Add(p18);
            PurchaseOrderItems p19 = new PurchaseOrderItems(); p19.POID = 17; p19.ItemID = 43; p19.OrderQty = 60; p19.ReceivedQty = 60; poItems.Add(p19);
            PurchaseOrderItems p20 = new PurchaseOrderItems(); p20.POID = 18; p20.ItemID = 48; p20.OrderQty = 500; p20.ReceivedQty = 500; poItems.Add(p20);
            PurchaseOrderItems p21 = new PurchaseOrderItems(); p21.POID = 19; p21.ItemID = 50; p21.OrderQty = 50; p21.ReceivedQty = 50; poItems.Add(p21);
            PurchaseOrderItems p22 = new PurchaseOrderItems(); p22.POID = 20; p22.ItemID = 52; p22.OrderQty = 50; p22.ReceivedQty = 50; poItems.Add(p22);
            PurchaseOrderItems p23 = new PurchaseOrderItems(); p23.POID = 21; p23.ItemID = 54; p23.OrderQty = 50; p23.ReceivedQty = 50; poItems.Add(p23);
            PurchaseOrderItems p24 = new PurchaseOrderItems(); p24.POID = 22; p24.ItemID = 56; p24.OrderQty = 50; p24.ReceivedQty = 50; poItems.Add(p24);
            PurchaseOrderItems p25 = new PurchaseOrderItems(); p25.POID = 23; p25.ItemID = 59; p25.OrderQty = 50; p25.ReceivedQty = 50; poItems.Add(p25);
            PurchaseOrderItems p26 = new PurchaseOrderItems(); p26.POID = 24; p26.ItemID = 62; p26.OrderQty = 50; p26.ReceivedQty = 50; poItems.Add(p26);
            PurchaseOrderItems p27 = new PurchaseOrderItems(); p27.POID = 25; p27.ItemID = 65; p27.OrderQty = 50; p27.ReceivedQty = 50; poItems.Add(p27);
            PurchaseOrderItems p28 = new PurchaseOrderItems(); p28.POID = 26; p28.ItemID = 70; p28.OrderQty = 80; p28.ReceivedQty = 80; poItems.Add(p28);
            PurchaseOrderItems p29 = new PurchaseOrderItems(); p29.POID = 27; p29.ItemID = 72; p29.OrderQty = 20; p29.ReceivedQty = 20; poItems.Add(p29);
            PurchaseOrderItems p30 = new PurchaseOrderItems(); p30.POID = 28; p30.ItemID = 74; p30.OrderQty = 20; p30.ReceivedQty = 20; poItems.Add(p30);
            PurchaseOrderItems p31 = new PurchaseOrderItems(); p31.POID = 29; p31.ItemID = 76; p31.OrderQty = 20; p31.ReceivedQty = 20; poItems.Add(p31);
            PurchaseOrderItems p32 = new PurchaseOrderItems(); p32.POID = 30; p32.ItemID = 80; p32.OrderQty = 10; p32.ReceivedQty = 10; poItems.Add(p32);
            PurchaseOrderItems p33 = new PurchaseOrderItems(); p33.POID = 31; p33.ItemID = 83; p33.OrderQty = 200; p33.ReceivedQty = 200; poItems.Add(p33);
            PurchaseOrderItems p34 = new PurchaseOrderItems(); p34.POID = 32; p34.ItemID = 87; p34.OrderQty = 200; p34.ReceivedQty = 200; poItems.Add(p34);
            PurchaseOrderItems p35 = new PurchaseOrderItems(); p35.POID = 33; p35.ItemID = 89; p35.OrderQty = 10; p35.ReceivedQty = 10; poItems.Add(p35);




            /*poItems.Add(p1);
            poItems.Add(p2);
            poItems.Add(p3);
            poItems.Add(p4);
            poItems.Add(p5);*/
            return poItems;
        }


    }
}
