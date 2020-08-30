using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LUSS_API.Models.PurchaseOrderStatus;

namespace LUSS_API.DB
{
    public class AddPO
    {
        public static List<PurchaseOrder> getAllPO(List<Supplier> suppliers, List<User> users)
        {
            List<PurchaseOrder> purchases = new List<PurchaseOrder>();

            /*            PurchaseOrder p1 = new PurchaseOrder()
                        {
                            CreatedOn = DateTime.Now.AddDays(-2),
                            ExpectedDate = DateTime.Now.AddDays(4),
                            PurchasedBy = users[8].UserID,
                            SupplierID = suppliers[0].SupplierID,
                            Status = POStatus.Pending,
                            PONo = "PO1"
                        };


                        PurchaseOrder p2 = new PurchaseOrder()
                        {
                            CreatedOn = DateTime.Now.AddDays(-8),
                            ExpectedDate = DateTime.Now.AddDays(-4),
                            PurchasedBy = users[8].UserID,
                            SupplierID = suppliers[4].SupplierID,
                            Status = POStatus.Completed,
                            ReceivedDate = DateTime.Now.AddDays(-2),
                            PONo = "PO2"
                        };

                        PurchaseOrder p3 = new PurchaseOrder()
                        {
                            CreatedOn = DateTime.Now.(-3),
                            ExpectedDate = DateTime.Now.AddDays(5),
                            PurchasedBy = users[8].UserID,
                            SupplierID = suppliers[3].SupplierID,
                            Status = POStatus.Pending,
                            PONo = "PO3"
                        };*/

            //History Data
            PurchaseOrder p1 = new PurchaseOrder(); p1.CreatedOn = DateTime.Now.AddDays(-132); p1.ExpectedDate = DateTime.Now.AddDays(-130); p1.PurchasedBy = users[8].UserID; p1.SupplierID = 4; p1.Status = POStatus.Completed; p1.ReceivedDate = DateTime.Now.AddDays(-125); p1.PONo = "PO1"; purchases.Add(p1);
            PurchaseOrder p2 = new PurchaseOrder(); p2.CreatedOn = DateTime.Now.AddDays(-102); p2.ExpectedDate = DateTime.Now.AddDays(-100); p2.PurchasedBy = users[8].UserID; p2.SupplierID = 5; p2.Status = POStatus.Completed; p2.ReceivedDate = DateTime.Now.AddDays(-95); p2.PONo = "PO2"; purchases.Add(p2);
            PurchaseOrder p3 = new PurchaseOrder(); p3.CreatedOn = DateTime.Now.AddDays(-42); p3.ExpectedDate = DateTime.Now.AddDays(-40); p3.PurchasedBy = users[8].UserID; p3.SupplierID = 1; p3.Status = POStatus.Completed; p3.ReceivedDate = DateTime.Now.AddDays(-35); p3.PONo = "PO3"; purchases.Add(p3);
            PurchaseOrder p4 = new PurchaseOrder(); p4.CreatedOn = DateTime.Now.AddDays(-22); p4.ExpectedDate = DateTime.Now.AddDays(-20); p4.PurchasedBy = users[8].UserID; p4.SupplierID = 2; p4.Status = POStatus.Completed; p4.ReceivedDate = DateTime.Now.AddDays(-15); p4.PONo = "PO4"; purchases.Add(p4);
            PurchaseOrder p5 = new PurchaseOrder(); p5.CreatedOn = DateTime.Now.AddDays(-150); p5.ExpectedDate = DateTime.Now.AddDays(-148); p5.PurchasedBy = users[8].UserID; p5.SupplierID = 1; p5.Status = POStatus.Completed; p5.ReceivedDate = DateTime.Now.AddDays(-143); p5.PONo = "PO5"; purchases.Add(p5);
            PurchaseOrder p6 = new PurchaseOrder(); p6.CreatedOn = DateTime.Now.AddDays(-117); p6.ExpectedDate = DateTime.Now.AddDays(-115); p6.PurchasedBy = users[8].UserID; p6.SupplierID = 5; p6.Status = POStatus.Completed; p6.ReceivedDate = DateTime.Now.AddDays(-110); p6.PONo = "PO6"; purchases.Add(p6);
            PurchaseOrder p7 = new PurchaseOrder(); p7.CreatedOn = DateTime.Now.AddDays(-353); p7.ExpectedDate = DateTime.Now.AddDays(-351); p7.PurchasedBy = users[8].UserID; p7.SupplierID = 7; p7.Status = POStatus.Completed; p7.ReceivedDate = DateTime.Now.AddDays(-346); p7.PONo = "PO7"; purchases.Add(p7);
            PurchaseOrder p8 = new PurchaseOrder(); p8.CreatedOn = DateTime.Now.AddDays(-43); p8.ExpectedDate = DateTime.Now.AddDays(-41); p8.PurchasedBy = users[8].UserID; p8.SupplierID = 6; p8.Status = POStatus.Completed; p8.ReceivedDate = DateTime.Now.AddDays(-36); p8.PONo = "PO8"; purchases.Add(p8);
            PurchaseOrder p9 = new PurchaseOrder(); p9.CreatedOn = DateTime.Now.AddDays(-191); p9.ExpectedDate = DateTime.Now.AddDays(-189); p9.PurchasedBy = users[8].UserID; p9.SupplierID = 2; p9.Status = POStatus.Completed; p9.ReceivedDate = DateTime.Now.AddDays(-184); p9.PONo = "PO9"; purchases.Add(p9);
            PurchaseOrder p10 = new PurchaseOrder(); p10.CreatedOn = DateTime.Now.AddDays(-239); p10.ExpectedDate = DateTime.Now.AddDays(-237); p10.PurchasedBy = users[8].UserID; p10.SupplierID = 7; p10.Status = POStatus.Completed; p10.ReceivedDate = DateTime.Now.AddDays(-232); p10.PONo = "PO10"; purchases.Add(p10);
            PurchaseOrder p11 = new PurchaseOrder(); p11.CreatedOn = DateTime.Now.AddDays(-227); p11.ExpectedDate = DateTime.Now.AddDays(-225); p11.PurchasedBy = users[8].UserID; p11.SupplierID = 4; p11.Status = POStatus.Completed; p11.ReceivedDate = DateTime.Now.AddDays(-220); p11.PONo = "PO11"; purchases.Add(p11);
            PurchaseOrder p12 = new PurchaseOrder(); p12.CreatedOn = DateTime.Now.AddDays(-290); p12.ExpectedDate = DateTime.Now.AddDays(-288); p12.PurchasedBy = users[8].UserID; p12.SupplierID = 9; p12.Status = POStatus.Completed; p12.ReceivedDate = DateTime.Now.AddDays(-283); p12.PONo = "PO12"; purchases.Add(p12);
            PurchaseOrder p13 = new PurchaseOrder(); p13.CreatedOn = DateTime.Now.AddDays(-172); p13.ExpectedDate = DateTime.Now.AddDays(-170); p13.PurchasedBy = users[8].UserID; p13.SupplierID = 9; p13.Status = POStatus.Completed; p13.ReceivedDate = DateTime.Now.AddDays(-165); p13.PONo = "PO13"; purchases.Add(p13);
            PurchaseOrder p14 = new PurchaseOrder(); p14.CreatedOn = DateTime.Now.AddDays(-194); p14.ExpectedDate = DateTime.Now.AddDays(-192); p14.PurchasedBy = users[8].UserID; p14.SupplierID = 7; p14.Status = POStatus.Completed; p14.ReceivedDate = DateTime.Now.AddDays(-187); p14.PONo = "PO14"; purchases.Add(p14);
            PurchaseOrder p15 = new PurchaseOrder(); p15.CreatedOn = DateTime.Now.AddDays(-26); p15.ExpectedDate = DateTime.Now.AddDays(-24); p15.PurchasedBy = users[8].UserID; p15.SupplierID = 2; p15.Status = POStatus.Completed; p15.ReceivedDate = DateTime.Now.AddDays(-19); p15.PONo = "PO15"; purchases.Add(p15);
            PurchaseOrder p16 = new PurchaseOrder(); p16.CreatedOn = DateTime.Now.AddDays(-276); p16.ExpectedDate = DateTime.Now.AddDays(-274); p16.PurchasedBy = users[8].UserID; p16.SupplierID = 1; p16.Status = POStatus.Completed; p16.ReceivedDate = DateTime.Now.AddDays(-269); p16.PONo = "PO16"; purchases.Add(p16);
            PurchaseOrder p17 = new PurchaseOrder(); p17.CreatedOn = DateTime.Now.AddDays(-276); p17.ExpectedDate = DateTime.Now.AddDays(-274); p17.PurchasedBy = users[8].UserID; p17.SupplierID = 2; p17.Status = POStatus.Completed; p17.ReceivedDate = DateTime.Now.AddDays(-269); p17.PONo = "PO17"; purchases.Add(p17);
            PurchaseOrder p18 = new PurchaseOrder(); p18.CreatedOn = DateTime.Now.AddDays(-16); p18.ExpectedDate = DateTime.Now.AddDays(-14); p18.PurchasedBy = users[8].UserID; p18.SupplierID = 6; p18.Status = POStatus.Completed; p18.ReceivedDate = DateTime.Now.AddDays(-9); p18.PONo = "PO18"; purchases.Add(p18);
            PurchaseOrder p19 = new PurchaseOrder(); p19.CreatedOn = DateTime.Now.AddDays(-283); p19.ExpectedDate = DateTime.Now.AddDays(-281); p19.PurchasedBy = users[8].UserID; p19.SupplierID = 3; p19.Status = POStatus.Completed; p19.ReceivedDate = DateTime.Now.AddDays(-276); p19.PONo = "PO19"; purchases.Add(p19);
            PurchaseOrder p20 = new PurchaseOrder(); p20.CreatedOn = DateTime.Now.AddDays(-313); p20.ExpectedDate = DateTime.Now.AddDays(-311); p20.PurchasedBy = users[8].UserID; p20.SupplierID = 5; p20.Status = POStatus.Completed; p20.ReceivedDate = DateTime.Now.AddDays(-306); p20.PONo = "PO20"; purchases.Add(p20);
            PurchaseOrder p21 = new PurchaseOrder(); p21.CreatedOn = DateTime.Now.AddDays(-340); p21.ExpectedDate = DateTime.Now.AddDays(-338); p21.PurchasedBy = users[8].UserID; p21.SupplierID = 3; p21.Status = POStatus.Completed; p21.ReceivedDate = DateTime.Now.AddDays(-333); p21.PONo = "PO21"; purchases.Add(p21);
            PurchaseOrder p22 = new PurchaseOrder(); p22.CreatedOn = DateTime.Now.AddDays(-141); p22.ExpectedDate = DateTime.Now.AddDays(-139); p22.PurchasedBy = users[8].UserID; p22.SupplierID = 6; p22.Status = POStatus.Completed; p22.ReceivedDate = DateTime.Now.AddDays(-134); p22.PONo = "PO22"; purchases.Add(p22);
            PurchaseOrder p23 = new PurchaseOrder(); p23.CreatedOn = DateTime.Now.AddDays(-345); p23.ExpectedDate = DateTime.Now.AddDays(-343); p23.PurchasedBy = users[8].UserID; p23.SupplierID = 10; p23.Status = POStatus.Completed; p23.ReceivedDate = DateTime.Now.AddDays(-338); p23.PONo = "PO23"; purchases.Add(p23);
            PurchaseOrder p24 = new PurchaseOrder(); p24.CreatedOn = DateTime.Now.AddDays(-246); p24.ExpectedDate = DateTime.Now.AddDays(-244); p24.PurchasedBy = users[8].UserID; p24.SupplierID = 1; p24.Status = POStatus.Completed; p24.ReceivedDate = DateTime.Now.AddDays(-239); p24.PONo = "PO24"; purchases.Add(p24);
            PurchaseOrder p25 = new PurchaseOrder(); p25.CreatedOn = DateTime.Now.AddDays(-157); p25.ExpectedDate = DateTime.Now.AddDays(-155); p25.PurchasedBy = users[8].UserID; p25.SupplierID = 3; p25.Status = POStatus.Completed; p25.ReceivedDate = DateTime.Now.AddDays(-150); p25.PONo = "PO25"; purchases.Add(p25);
            PurchaseOrder p26 = new PurchaseOrder(); p26.CreatedOn = DateTime.Now.AddDays(-350); p26.ExpectedDate = DateTime.Now.AddDays(-348); p26.PurchasedBy = users[8].UserID; p26.SupplierID = 4; p26.Status = POStatus.Completed; p26.ReceivedDate = DateTime.Now.AddDays(-343); p26.PONo = "PO26"; purchases.Add(p26);
            PurchaseOrder p27 = new PurchaseOrder(); p27.CreatedOn = DateTime.Now.AddDays(-244); p27.ExpectedDate = DateTime.Now.AddDays(-242); p27.PurchasedBy = users[8].UserID; p27.SupplierID = 4; p27.Status = POStatus.Completed; p27.ReceivedDate = DateTime.Now.AddDays(-237); p27.PONo = "PO27"; purchases.Add(p27);
            PurchaseOrder p28 = new PurchaseOrder(); p28.CreatedOn = DateTime.Now.AddDays(-136); p28.ExpectedDate = DateTime.Now.AddDays(-134); p28.PurchasedBy = users[8].UserID; p28.SupplierID = 6; p28.Status = POStatus.Completed; p28.ReceivedDate = DateTime.Now.AddDays(-129); p28.PONo = "PO28"; purchases.Add(p28);
            PurchaseOrder p29 = new PurchaseOrder(); p29.CreatedOn = DateTime.Now.AddDays(-24); p29.ExpectedDate = DateTime.Now.AddDays(-22); p29.PurchasedBy = users[8].UserID; p29.SupplierID = 5; p29.Status = POStatus.Completed; p29.ReceivedDate = DateTime.Now.AddDays(-17); p29.PONo = "PO29"; purchases.Add(p29);
            PurchaseOrder p30 = new PurchaseOrder(); p30.CreatedOn = DateTime.Now.AddDays(-132); p30.ExpectedDate = DateTime.Now.AddDays(-130); p30.PurchasedBy = users[8].UserID; p30.SupplierID = 6; p30.Status = POStatus.Completed; p30.ReceivedDate = DateTime.Now.AddDays(-125); p30.PONo = "PO30"; purchases.Add(p30);
            PurchaseOrder p31 = new PurchaseOrder(); p31.CreatedOn = DateTime.Now.AddDays(-159); p31.ExpectedDate = DateTime.Now.AddDays(-157); p31.PurchasedBy = users[8].UserID; p31.SupplierID = 5; p31.Status = POStatus.Completed; p31.ReceivedDate = DateTime.Now.AddDays(-152); p31.PONo = "PO31"; purchases.Add(p31);
            PurchaseOrder p32 = new PurchaseOrder(); p32.CreatedOn = DateTime.Now.AddDays(-132); p32.ExpectedDate = DateTime.Now.AddDays(-130); p32.PurchasedBy = users[8].UserID; p32.SupplierID = 7; p32.Status = POStatus.Completed; p32.ReceivedDate = DateTime.Now.AddDays(-125); p32.PONo = "PO32"; purchases.Add(p32);
            PurchaseOrder p33 = new PurchaseOrder(); p33.CreatedOn = DateTime.Now.AddDays(-243); p33.ExpectedDate = DateTime.Now.AddDays(-241); p33.PurchasedBy = users[8].UserID; p33.SupplierID = 6; p33.Status = POStatus.Completed; p33.ReceivedDate = DateTime.Now.AddDays(-236); p33.PONo = "PO33"; purchases.Add(p33);

            /*

                        purchases.Add(p1);
                        purchases.Add(p2);
                        purchases.Add(p3);*/

            return purchases;
        }
    }
}
