using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.DB
{
    public class AddRequestDetail
    {
        public static List<RequestDetails> getAllRequestDetails(List<Item> items, List<Request> requests)
        {
            List<RequestDetails> requestDetails = new List<RequestDetails>();

            //History Data: completed
            RequestDetails del1 = new RequestDetails(); del1.RequestQty = 5; del1.ItemID = 2; del1.RequestID = 1; del1.FullfillQty = 5; del1.ReceivedQty = 5; requestDetails.Add(del1);
            RequestDetails del2 = new RequestDetails(); del2.RequestQty = 7; del2.ItemID = 10; del2.RequestID = 1; del2.FullfillQty = 7; del2.ReceivedQty = 7; requestDetails.Add(del2);
            RequestDetails del3 = new RequestDetails(); del3.RequestQty = 5; del3.ItemID = 35; del3.RequestID = 2; del3.FullfillQty = 5; del3.ReceivedQty = 5; requestDetails.Add(del3);
            RequestDetails del4 = new RequestDetails(); del4.RequestQty = 4; del4.ItemID = 56; del4.RequestID = 2; del4.FullfillQty = 4; del4.ReceivedQty = 4; requestDetails.Add(del4);
            RequestDetails del5 = new RequestDetails(); del5.RequestQty = 5; del5.ItemID = 50; del5.RequestID = 2; del5.FullfillQty = 5; del5.ReceivedQty = 5; requestDetails.Add(del5);
            RequestDetails del6 = new RequestDetails(); del6.RequestQty = 4; del6.ItemID = 70; del6.RequestID = 2; del6.FullfillQty = 4; del6.ReceivedQty = 4; requestDetails.Add(del6);
            RequestDetails del7 = new RequestDetails(); del7.RequestQty = 7; del7.ItemID = 84; del7.RequestID = 3; del7.FullfillQty = 7; del7.ReceivedQty = 7; requestDetails.Add(del7);
            RequestDetails del8 = new RequestDetails(); del8.RequestQty = 1; del8.ItemID = 35; del8.RequestID = 3; del8.FullfillQty = 1; del8.ReceivedQty = 1; requestDetails.Add(del8);
            RequestDetails del9 = new RequestDetails(); del9.RequestQty = 42; del9.ItemID = 12; del9.RequestID = 4; del9.FullfillQty = 42; del9.ReceivedQty = 42; requestDetails.Add(del9);
            RequestDetails del10 = new RequestDetails(); del10.RequestQty = 5; del10.ItemID = 67; del10.RequestID = 4; del10.FullfillQty = 5; del10.ReceivedQty = 5; requestDetails.Add(del10);
            RequestDetails del11 = new RequestDetails(); del11.RequestQty = 30; del11.ItemID = 65; del11.RequestID = 4; del11.FullfillQty = 30; del11.ReceivedQty = 30; requestDetails.Add(del11);
            RequestDetails del12 = new RequestDetails(); del12.RequestQty = 40; del12.ItemID = 71; del12.RequestID = 4; del12.FullfillQty = 40; del12.ReceivedQty = 40; requestDetails.Add(del12);
            RequestDetails del13 = new RequestDetails(); del13.RequestQty = 8; del13.ItemID = 84; del13.RequestID = 5; del13.FullfillQty = 8; del13.ReceivedQty = 8; requestDetails.Add(del13);
            RequestDetails del14 = new RequestDetails(); del14.RequestQty = 5; del14.ItemID = 24; del14.RequestID = 5; del14.FullfillQty = 5; del14.ReceivedQty = 5; requestDetails.Add(del14);
            RequestDetails del15 = new RequestDetails(); del15.RequestQty = 5; del15.ItemID = 30; del15.RequestID = 5; del15.FullfillQty = 5; del15.ReceivedQty = 5; requestDetails.Add(del15);
            RequestDetails del16 = new RequestDetails(); del16.RequestQty = 2; del16.ItemID = 89; del16.RequestID = 6; del16.FullfillQty = 2; del16.ReceivedQty = 2; requestDetails.Add(del16);
            RequestDetails del17 = new RequestDetails(); del17.RequestQty = 5; del17.ItemID = 13; del17.RequestID = 6; del17.FullfillQty = 5; del17.ReceivedQty = 5; requestDetails.Add(del17);
            RequestDetails del18 = new RequestDetails(); del18.RequestQty = 2; del18.ItemID = 4; del18.RequestID = 7; del18.FullfillQty = 2; del18.ReceivedQty = 2; requestDetails.Add(del18);
            RequestDetails del19 = new RequestDetails(); del19.RequestQty = 40; del19.ItemID = 10; del19.RequestID = 7; del19.FullfillQty = 40; del19.ReceivedQty = 40; requestDetails.Add(del19);
            RequestDetails del20 = new RequestDetails(); del20.RequestQty = 3; del20.ItemID = 74; del20.RequestID = 7; del20.FullfillQty = 3; del20.ReceivedQty = 3; requestDetails.Add(del20);
            RequestDetails del21 = new RequestDetails(); del21.RequestQty = 1; del21.ItemID = 87; del21.RequestID = 8; del21.FullfillQty = 1; del21.ReceivedQty = 1; requestDetails.Add(del21);
            RequestDetails del22 = new RequestDetails(); del22.RequestQty = 1; del22.ItemID = 85; del22.RequestID = 8; del22.FullfillQty = 1; del22.ReceivedQty = 1; requestDetails.Add(del22);
            RequestDetails del23 = new RequestDetails(); del23.RequestQty = 1; del23.ItemID = 63; del23.RequestID = 8; del23.FullfillQty = 1; del23.ReceivedQty = 1; requestDetails.Add(del23);
            RequestDetails del24 = new RequestDetails(); del24.RequestQty = 1; del24.ItemID = 67; del24.RequestID = 8; del24.FullfillQty = 1; del24.ReceivedQty = 1; requestDetails.Add(del24);
            RequestDetails del25 = new RequestDetails(); del25.RequestQty = 10; del25.ItemID = 26; del25.RequestID = 9; del25.FullfillQty = 10; del25.ReceivedQty = 10; requestDetails.Add(del25);
            RequestDetails del26 = new RequestDetails(); del26.RequestQty = 10; del26.ItemID = 27; del26.RequestID = 9; del26.FullfillQty = 10; del26.ReceivedQty = 10; requestDetails.Add(del26);
            RequestDetails del27 = new RequestDetails(); del27.RequestQty = 10; del27.ItemID = 28; del27.RequestID = 9; del27.FullfillQty = 10; del27.ReceivedQty = 10; requestDetails.Add(del27);
            RequestDetails del28 = new RequestDetails(); del28.RequestQty = 10; del28.ItemID = 29; del28.RequestID = 9; del28.FullfillQty = 10; del28.ReceivedQty = 10; requestDetails.Add(del28);
            RequestDetails del29 = new RequestDetails(); del29.RequestQty = 4; del29.ItemID = 9; del29.RequestID = 10; del29.FullfillQty = 4; del29.ReceivedQty = 4; requestDetails.Add(del29);
            RequestDetails del30 = new RequestDetails(); del30.RequestQty = 4; del30.ItemID = 1; del30.RequestID = 10; del30.FullfillQty = 4; del30.ReceivedQty = 4; requestDetails.Add(del30);
            RequestDetails del31 = new RequestDetails(); del31.RequestQty = 2; del31.ItemID = 4; del31.RequestID = 10; del31.FullfillQty = 2; del31.ReceivedQty = 2; requestDetails.Add(del31);
            RequestDetails del32 = new RequestDetails(); del32.RequestQty = 10; del32.ItemID = 8; del32.RequestID = 11; del32.FullfillQty = 10; del32.ReceivedQty = 10; requestDetails.Add(del32);
            RequestDetails del33 = new RequestDetails(); del33.RequestQty = 10; del33.ItemID = 7; del33.RequestID = 11; del33.FullfillQty = 10; del33.ReceivedQty = 10; requestDetails.Add(del33);
            RequestDetails del34 = new RequestDetails(); del34.RequestQty = 10; del34.ItemID = 11; del34.RequestID = 11; del34.FullfillQty = 10; del34.ReceivedQty = 10; requestDetails.Add(del34);
            RequestDetails del35 = new RequestDetails(); del35.RequestQty = 10; del35.ItemID = 12; del35.RequestID = 11; del35.FullfillQty = 10; del35.ReceivedQty = 10; requestDetails.Add(del35);
            RequestDetails del36 = new RequestDetails(); del36.RequestQty = 3; del36.ItemID = 7; del36.RequestID = 12; del36.FullfillQty = 3; del36.ReceivedQty = 3; requestDetails.Add(del36);
            RequestDetails del37 = new RequestDetails(); del37.RequestQty = 5; del37.ItemID = 4; del37.RequestID = 12; del37.FullfillQty = 5; del37.ReceivedQty = 5; requestDetails.Add(del37);
            RequestDetails del38 = new RequestDetails(); del38.RequestQty = 2; del38.ItemID = 2; del38.RequestID = 12; del38.FullfillQty = 2; del38.ReceivedQty = 2; requestDetails.Add(del38);
            RequestDetails del39 = new RequestDetails(); del39.RequestQty = 1; del39.ItemID = 89; del39.RequestID = 13; del39.FullfillQty = 1; del39.ReceivedQty = 1; requestDetails.Add(del39);
            RequestDetails del40 = new RequestDetails(); del40.RequestQty = 1; del40.ItemID = 67; del40.RequestID = 13; del40.FullfillQty = 1; del40.ReceivedQty = 1; requestDetails.Add(del40);
            RequestDetails del41 = new RequestDetails(); del41.RequestQty = 4; del41.ItemID = 48; del41.RequestID = 13; del41.FullfillQty = 4; del41.ReceivedQty = 4; requestDetails.Add(del41);
            RequestDetails del42 = new RequestDetails(); del42.RequestQty = 2; del42.ItemID = 39; del42.RequestID = 14; del42.FullfillQty = 2; del42.ReceivedQty = 2; requestDetails.Add(del42);
            RequestDetails del43 = new RequestDetails(); del43.RequestQty = 10; del43.ItemID = 37; del43.RequestID = 14; del43.FullfillQty = 10; del43.ReceivedQty = 10; requestDetails.Add(del43);
            RequestDetails del44 = new RequestDetails(); del44.RequestQty = 5; del44.ItemID = 42; del44.RequestID = 14; del44.FullfillQty = 5; del44.ReceivedQty = 5; requestDetails.Add(del44);
            RequestDetails del45 = new RequestDetails(); del45.RequestQty = 2; del45.ItemID = 48; del45.RequestID = 14; del45.FullfillQty = 2; del45.ReceivedQty = 2; requestDetails.Add(del45);
            RequestDetails del46 = new RequestDetails(); del46.RequestQty = 5; del46.ItemID = 42; del46.RequestID = 15; del46.FullfillQty = 5; del46.ReceivedQty = 5; requestDetails.Add(del46);
            RequestDetails del47 = new RequestDetails(); del47.RequestQty = 4; del47.ItemID = 47; del47.RequestID = 15; del47.FullfillQty = 4; del47.ReceivedQty = 4; requestDetails.Add(del47);
            RequestDetails del48 = new RequestDetails(); del48.RequestQty = 5; del48.ItemID = 18; del48.RequestID = 15; del48.FullfillQty = 5; del48.ReceivedQty = 5; requestDetails.Add(del48);
            RequestDetails del49 = new RequestDetails(); del49.RequestQty = 4; del49.ItemID = 23; del49.RequestID = 15; del49.FullfillQty = 4; del49.ReceivedQty = 4; requestDetails.Add(del49);
            RequestDetails del50 = new RequestDetails(); del50.RequestQty = 1; del50.ItemID = 48; del50.RequestID = 15; del50.FullfillQty = 1; del50.ReceivedQty = 1; requestDetails.Add(del50);
            RequestDetails del51 = new RequestDetails(); del51.RequestQty = 4; del51.ItemID = 54; del51.RequestID = 16; del51.FullfillQty = 4; del51.ReceivedQty = 4; requestDetails.Add(del51);
            RequestDetails del52 = new RequestDetails(); del52.RequestQty = 4; del52.ItemID = 56; del52.RequestID = 16; del52.FullfillQty = 4; del52.ReceivedQty = 4; requestDetails.Add(del52);
            RequestDetails del53 = new RequestDetails(); del53.RequestQty = 2; del53.ItemID = 62; del53.RequestID = 16; del53.FullfillQty = 2; del53.ReceivedQty = 2; requestDetails.Add(del53);
            RequestDetails del54 = new RequestDetails(); del54.RequestQty = 10; del54.ItemID = 70; del54.RequestID = 16; del54.FullfillQty = 10; del54.ReceivedQty = 10; requestDetails.Add(del54);
            RequestDetails del55 = new RequestDetails(); del55.RequestQty = 10; del55.ItemID = 15; del55.RequestID = 17; del55.FullfillQty = 10; del55.ReceivedQty = 10; requestDetails.Add(del55);
            RequestDetails del56 = new RequestDetails(); del56.RequestQty = 15; del56.ItemID = 18; del56.RequestID = 17; del56.FullfillQty = 15; del56.ReceivedQty = 15; requestDetails.Add(del56);
            RequestDetails del57 = new RequestDetails(); del57.RequestQty = 2; del57.ItemID = 24; del57.RequestID = 17; del57.FullfillQty = 2; del57.ReceivedQty = 2; requestDetails.Add(del57);
            RequestDetails del58 = new RequestDetails(); del58.RequestQty = 1; del58.ItemID = 89; del58.RequestID = 17; del58.FullfillQty = 1; del58.ReceivedQty = 1; requestDetails.Add(del58);
            RequestDetails del59 = new RequestDetails(); del59.RequestQty = 1; del59.ItemID = 37; del59.RequestID = 18; del59.FullfillQty = 1; del59.ReceivedQty = 1; requestDetails.Add(del59);
            RequestDetails del60 = new RequestDetails(); del60.RequestQty = 3; del60.ItemID = 78; del60.RequestID = 18; del60.FullfillQty = 3; del60.ReceivedQty = 3; requestDetails.Add(del60);
            RequestDetails del61 = new RequestDetails(); del61.RequestQty = 3; del61.ItemID = 70; del61.RequestID = 18; del61.FullfillQty = 3; del61.ReceivedQty = 3; requestDetails.Add(del61);
            RequestDetails del62 = new RequestDetails(); del62.RequestQty = 6; del62.ItemID = 76; del62.RequestID = 18; del62.FullfillQty = 6; del62.ReceivedQty = 6; requestDetails.Add(del62);
            //new request details
            RequestDetails del63 = new RequestDetails(); del63.RequestQty = 3; del63.ItemID = 79; del63.RequestID = 19; del63.FullfillQty = null; del63.ReceivedQty = null; requestDetails.Add(del63);
            RequestDetails del64 = new RequestDetails(); del64.RequestQty = 1; del64.ItemID = 76; del64.RequestID = 19; del64.FullfillQty = null; del64.ReceivedQty = null; requestDetails.Add(del64);
            RequestDetails del65 = new RequestDetails(); del65.RequestQty = 3; del65.ItemID = 47; del65.RequestID = 20; del65.FullfillQty = null; del65.ReceivedQty = null; requestDetails.Add(del65);
            RequestDetails del66 = new RequestDetails(); del66.RequestQty = 5; del66.ItemID = 68; del66.RequestID = 20; del66.FullfillQty = null; del66.ReceivedQty = null; requestDetails.Add(del66);
            RequestDetails del67 = new RequestDetails(); del67.RequestQty = 4; del67.ItemID = 82; del67.RequestID = 20; del67.FullfillQty = null; del67.ReceivedQty = null; requestDetails.Add(del67);
            RequestDetails del68 = new RequestDetails(); del68.RequestQty = 4; del68.ItemID = 35; del68.RequestID = 21; del68.FullfillQty = 4; del68.ReceivedQty = 4; requestDetails.Add(del68);
            RequestDetails del69 = new RequestDetails(); del69.RequestQty = 2; del69.ItemID = 36; del69.RequestID = 21; del69.FullfillQty = 2; del69.ReceivedQty = 2; requestDetails.Add(del69);
            RequestDetails del70 = new RequestDetails(); del70.RequestQty = 2; del70.ItemID = 49; del70.RequestID = 21; del70.FullfillQty = 2; del70.ReceivedQty = 2; requestDetails.Add(del70);
            RequestDetails del71 = new RequestDetails(); del71.RequestQty = 2; del71.ItemID = 3; del71.RequestID = 22; del71.FullfillQty = 2; del71.ReceivedQty = null; requestDetails.Add(del71);
            RequestDetails del72 = new RequestDetails(); del72.RequestQty = 1; del72.ItemID = 10; del72.RequestID = 22; del72.FullfillQty = 1; del72.ReceivedQty = null; requestDetails.Add(del72);
            RequestDetails del73 = new RequestDetails(); del73.RequestQty = 2; del73.ItemID = 48; del73.RequestID = 23; del73.FullfillQty = 2; del73.ReceivedQty = 2; requestDetails.Add(del73);
            RequestDetails del74 = new RequestDetails(); del74.RequestQty = 3; del74.ItemID = 84; del74.RequestID = 23; del74.FullfillQty = 3; del74.ReceivedQty = 3; requestDetails.Add(del74);
            RequestDetails del75 = new RequestDetails(); del75.RequestQty = 3; del75.ItemID = 70; del75.RequestID = 23; del75.FullfillQty = 3; del75.ReceivedQty = 3; requestDetails.Add(del75);
            RequestDetails del76 = new RequestDetails(); del76.RequestQty = 3; del76.ItemID = 3; del76.RequestID = 23; del76.FullfillQty = 3; del76.ReceivedQty = 3; requestDetails.Add(del76);
            RequestDetails del77 = new RequestDetails(); del77.RequestQty = 1; del77.ItemID = 79; del77.RequestID = 24; del77.FullfillQty = null; del77.ReceivedQty = null; requestDetails.Add(del77);
            RequestDetails del78 = new RequestDetails(); del78.RequestQty = 1; del78.ItemID = 34; del78.RequestID = 24; del78.FullfillQty = null; del78.ReceivedQty = null; requestDetails.Add(del78);
            RequestDetails del79 = new RequestDetails(); del79.RequestQty = 1; del79.ItemID = 4; del79.RequestID = 25; del79.FullfillQty = null; del79.ReceivedQty = null; requestDetails.Add(del79);
            RequestDetails del80 = new RequestDetails(); del80.RequestQty = 3; del80.ItemID = 3; del80.RequestID = 25; del80.FullfillQty = null; del80.ReceivedQty = null; requestDetails.Add(del80);

            //forRq26
                       
            RequestDetails del81 = new RequestDetails(); del81.RequestQty = 4; del81.ItemID = 6; del81.RequestID = 26; del81.FullfillQty = 3; del81.ReceivedQty = null; requestDetails.Add(del81);
            RequestDetails del82 = new RequestDetails(); del82.RequestQty = 6; del82.ItemID = 62; del82.RequestID = 26; del82.FullfillQty = 6; del82.ReceivedQty = null; requestDetails.Add(del82);
            //forRq27
            RequestDetails del83 = new RequestDetails(); del83.RequestQty = 1; del83.ItemID = 56; del83.RequestID = 27; del83.FullfillQty = 1; del83.ReceivedQty = null; requestDetails.Add(del83);
            RequestDetails del84 = new RequestDetails(); del84.RequestQty = 16; del84.ItemID = 62; del84.RequestID = 27; del84.FullfillQty = 6; del84.ReceivedQty = null; requestDetails.Add(del84);


            return requestDetails;
        }
    }
}
