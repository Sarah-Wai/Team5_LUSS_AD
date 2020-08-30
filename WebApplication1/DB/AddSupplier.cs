using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.Models;

namespace LUSS_API.DB
{
    public class AddSupplier
    {
        public static List<Supplier> getAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            Supplier supplier1 = new Supplier()
            {
                SupplierID = 1,
                SupplierName = "ALPHA Office Supplies",
                Address = "Blk 1128 Ang Mo Kio Industrial Park #02-1108 Ang Mo Kio Street 62 Singapore 622262",
                ContactNo = "461 9928",
                email = "ALPHA@gmail.com",
                Description = "Good Service "
            };
            Supplier supplier2 = new Supplier()
            {
                SupplierID = 2,
                SupplierName = "Astellas Pharma US",
                Address = "Blk 252 S Harrison Ave, South Beloit, IL, 61080",
                ContactNo = "661 3452",
                email = "damienTerrell@gmail.com",
                Description = "Good Service "
            };
            Supplier supplier3 = new Supplier()
            {
                SupplierID = 3,
                SupplierName = "BANES Shop",
                Address = "Blk 124 Alexandra Road #03-04 Banes Building Singapore 550315",
                ContactNo = "478 1234",
                email = "ALPHA@gmail.com",
                Description = "Good Service "
            };
            Supplier supplier4 = new Supplier()
            {
                SupplierID = 4,
                SupplierName = "Boehringer Ingelheim",
                Address = "Blk 279 NE 12th Ave, Homestead, FL, 33030",
                ContactNo = "171 7162",
                email = "ALPHA@gmail.com",
                Description = "Good Service "
            };
            Supplier supplier5 = new Supplier()
            {
                SupplierID = 5,
                SupplierName = "Boeing AeroSpace",
                Address = "Blk 102 W State Rd #28, West Lebanon, IN, 47991",
                ContactNo = "881 3369",
                email = "ALPHA@gmail.com",
                Description = "Good Service "
            };
            Supplier supplier6 = new Supplier()
            {
                SupplierID = 6,
                SupplierName = "Bristol-Myers Squibb",
                Address = "Blk 153 Mississippi River Rd #BL, Brownsville, TX, 78520",
                ContactNo = "112 1313",
                email = "ALPHA@gmail.com",
                Description = "Good Service "
            };
            Supplier supplier7 = new Supplier()
            {
                SupplierID = 7,
                SupplierName = "Cheap Stationer",
                Address = "Blk 34 Clementi Road #07-02 Ban Ban Soh Building Singapore 110525",
                ContactNo = "354 3234",
                email = "ALPHA@gmail.com",
                Description = "Good Service "
            };
            Supplier supplier8 = new Supplier()
            {
                SupplierID = 8,
                SupplierName = "Centocor Ortho Biotech, Inc.",
                Address = "Blk 111 E Jeff Davis Hwy, Elkton, KY, 42220",
                ContactNo = "369 7812",
                email = "ALPHA@gmail.com",
                Description = "Good Service "
            };
            Supplier supplier9 = new Supplier()
            {
                SupplierID = 9,
                SupplierName = "Genentech, Novartis",
                Address = "Blk 807 Kingdom Come Dr, Cumberland, KY, 40823",
                ContactNo = "454 9918",
                email = "ALPHA@gmail.com",
                Description = "Good Service "
            };
            Supplier supplier10 = new Supplier()
            {
                SupplierID = 10,
                SupplierName = "OMEGA Stationery Supplier",
                Address = "Blk 11 Hillview Avenue #03-04 Singapore 679036",
                ContactNo = "767 1233",
                email = "ALPHA@gmail.com",
                Description = "Good Service "
            };
           
           

            suppliers.Add(supplier1);
            suppliers.Add(supplier2);
            suppliers.Add(supplier3);
            suppliers.Add(supplier4);
            suppliers.Add(supplier5);
            suppliers.Add(supplier6);
            suppliers.Add(supplier7);
            suppliers.Add(supplier8);
            suppliers.Add(supplier9);
            suppliers.Add(supplier10);

            return suppliers;
        }
    }
}
