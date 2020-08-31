# Team5_LUSS_AD
ASP.Net project repository Developed By SA50_Team5 

This Web application is developed for Logic University to perform daily operation including:
Secure login/logout
Dashboard and report generation
Email and notification 
Employee: to view and raise stationery request; update, cancel request;
DeparmentHead: to approve/reject department requests; to assign/remove delegate; to assign representative; 
DeparmtentRepresentative: to collect department stationery; to update collection point;
StoreManager/Supervisor: to view item info; to approve/reject adjustment vouchers;
StoreClerk: to manage disbursement; adjust stock; confirm delivery; raise PO and receive PO

This Web application is seeded with data in order for users to perform necessary actions using AWS cloud database. 
You dont need to seed the data unless you need to save them into local database. Please change the setting and uncomment out the seeding code in Startup and appsetting.json

To access to the Web Application, user needs to login with employee email and password. Here are the demo login (default pwd: 123):
DepartmentHead: wai@gmail.com
DepartmentRep: nang@gmail.com
DepartmentDelegate: sarah@gmail.com
StoreClerk: weekiat@gmail.com
StoreSupervisor: selly@gmail.com
StoreManager: sherren@gmail.com
