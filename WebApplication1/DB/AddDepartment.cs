using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.Models;

namespace LUSS_API.DB
{
    public class AddDepartment
    {
        public static List<Department> getAllDepartment(List<CollectionPoint> collectionPoints)
        {
            List<Department> departments = new List<Department>();

            Department dep1 = new Department()
            {
                DepartmentID = 1,
                DepartmentCode= "CLAI",
                DepartmentName = "Claims Department",
                CollectionPointID= collectionPoints[0].CollectionPointID,
                Fax= "867 3311",
                PhoneNo= "817 3311"
            };

            Department dep2 = new Department()
            {
                DepartmentID = 2,
                DepartmentCode = "COMM",//check
                DepartmentName = "Commerce Department",
                CollectionPointID = collectionPoints[0].CollectionPointID,
                Fax = "874 1284",
                PhoneNo = "892 1256"
            };

            Department dep3 = new Department()//check
            {
                DepartmentID = 3,
                DepartmentCode = "CPSC",
                DepartmentName = "Computer Science",
                CollectionPointID = collectionPoints[1].CollectionPointID,
                Fax = "890 1235",
                PhoneNo = "892 1457"
            };

            Department dep4 = new Department()//check
            {
                DepartmentID = 4,
                DepartmentCode = "ENGL",
                DepartmentName = "English Department",
                CollectionPointID = collectionPoints[2].CollectionPointID,
                Fax = "874 2234",
                PhoneNo = "892 1456"
            };

            Department dep5 = new Department()
            {
                DepartmentID = 5,
                DepartmentCode = "ESTS",
                DepartmentName = "Real Estate Department",
                CollectionPointID = collectionPoints[3].CollectionPointID,
                Fax = "864 2311",
                PhoneNo = "890 1131"
            };

            Department dep6 = new Department()
            {
                DepartmentID = 6,
                DepartmentCode = "FINN",
                DepartmentName = "Finance Department",
                CollectionPointID = collectionPoints[4].CollectionPointID,
                Fax = "811 1123",
                PhoneNo = "813 7643"
            };

            Department dep7 = new Department()//check
            {
                DepartmentID = 7,
                DepartmentCode = "REGR",
                DepartmentName = "Registrar Department",
                CollectionPointID = collectionPoints[5].CollectionPointID,
                Fax = "892 1465",
                PhoneNo = "890 1266"
            };

            Department dep8 = new Department()//check
            {
                DepartmentID = 8,
                DepartmentCode = "CLAI",
                DepartmentName = "Claims Department",
                CollectionPointID = collectionPoints[1].CollectionPointID,
                Fax = "867 3311",
                PhoneNo = "817 3311"
            };

            Department dep9 = new Department()//check
            {
                DepartmentID = 9,
                DepartmentCode = "ZOOL",
                DepartmentName = "Zoology Department",
                CollectionPointID = collectionPoints[2].CollectionPointID,
                Fax = "892 1465",
                PhoneNo = "890 1266"
            };

            Department dep10 = new Department()//check
            {
                DepartmentID = 10,
                DepartmentCode = "STOR",
                DepartmentName = "Store Department",
                CollectionPointID = collectionPoints[3].CollectionPointID,
                Fax = "867 3311",
                PhoneNo = "817 3311"
            };
            /*
            Department dep11 = new Department()
            {
                DepartmentID = 11,
                DepartmentCode = "CLAI",
                DepartmentName = "Claims Department",
                CollectionPointID = collectionPoints[0].CollectionPointID,
                Fax = "867 3311",
                PhoneNo = "817 3311"
            };*/
            departments.Add(dep1);
            departments.Add(dep2);
            departments.Add(dep3);
            departments.Add(dep4);
            departments.Add(dep5);
            departments.Add(dep6);
            departments.Add(dep7);
            departments.Add(dep8);
            departments.Add(dep9);
            departments.Add(dep10);
            //departments.Add(dep11);
            return departments;
        }
    }
}
