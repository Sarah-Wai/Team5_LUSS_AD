using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.Models;

namespace LUSS_API.DB
{
    public class AddCollectionPoints
    {
        public static List<CollectionPoint> getAllCollectionPoints()
        {
            List<CollectionPoint> collectionPoints = new List<CollectionPoint>();

            CollectionPoint collection1 = new CollectionPoint();
            collection1.CollectionPointID = 1;
            collection1.Description = "9:30:00 AM";
            collection1.Location = "Stationery Store, Administration Building";
            collection1.PointName = "1";

            CollectionPoint collection2 = new CollectionPoint();
            collection2.CollectionPointID = 2;
            collection2.Description = "11:00:00 AM";
            collection2.Location = "Management School";
            collection2.PointName = "2";

            CollectionPoint collection3 = new CollectionPoint();
            collection3.CollectionPointID = 3;
            collection3.Description = "9:30:00 AM";
            collection3.Location = "Medical School";
            collection3.PointName = "3";

            CollectionPoint collection4 = new CollectionPoint();
            collection4.CollectionPointID = 4;
            collection4.Description = "11:00:00 AM";
            collection4.Location = "Engineering School";
            collection4.PointName = "4";

            CollectionPoint collection5 = new CollectionPoint();
            collection5.CollectionPointID = 5;
            collection5.Description = "9:30:00 AM";
            collection5.Location = "Science School";
            collection5.PointName = "5";

            CollectionPoint collection6 = new CollectionPoint();
            collection6.CollectionPointID = 6;
            collection6.Description = "11:00:00 AM";
            collection6.Location = "University Hospital";
            collection6.PointName = "6";

            collectionPoints.Add(collection1);
            collectionPoints.Add(collection2);
            collectionPoints.Add(collection3);
            collectionPoints.Add(collection4);
            collectionPoints.Add(collection5);
            collectionPoints.Add(collection6);
            return collectionPoints;
        }
    }
}
