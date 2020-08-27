using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Globalization;
using System.Net.Http;

namespace LUSS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DelegateController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<DelegateController> _logger;
        public DelegateController(ILogger<DelegateController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }


        [HttpGet("{id}")]
        [Route("getDelegateByUserID/{id}")]
        public DelegatedManager getDelegateByUserID(int id)
        {
            try
            {
                DelegatedManager delegatedManager = context123.DelegatedManager.First(c => c.UserID == id);
                return delegatedManager;

            }
            catch (Exception ex) { return null; }


        }

        [HttpGet("{depID}")]
        [Route("getCurrentDelegate/{depID}")]
        public dynamic getCurrentDelegate(int depID)
        {
            try
            {
                DelegatedManager return_delegated = new DelegatedManager();
                User current_delegatedUser = new User();
                List<int> users = context123.User.Where(x => x.DepartmentID == depID).Select(c => c.UserID).ToList();
                List<DelegatedManager> delegated_list = context123.DelegatedManager.Where(x => users.Contains(x.UserID) && x.FromDate >=DateTime.Now.Date).OrderBy(x=>x.FromDate).ToList();  //current active delegate
                if (delegated_list.Count > 0)
                {
                    DelegatedManager current_active_delegate = delegated_list.Where(x => x.isActive == true).FirstOrDefault();

                    if (current_active_delegate != null)
                    {
                        return_delegated = current_active_delegate;

                    }
                    else
                    {   //no current active delegate
                        return_delegated = delegated_list[0];

                    }
                    if (return_delegated.User != null)
                    {
                        current_delegatedUser = new User { UserID = return_delegated.User.UserID, FirstName = return_delegated.User.FirstName, LastName = return_delegated.User.LastName };
                    }
                }
                    List<User> Users = context123.User.Where(x => x.DepartmentID == depID).ToList();
                List<User> return_users = new List<User>();
                foreach (User u in Users)
                {
                    User newUser = new User()
                    {
                        UserID = u.UserID,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    };
                    return_users.Add(newUser);
                }


                dynamic return_value = new System.Dynamic.ExpandoObject();
                return_value.delegatedManagerID = return_delegated.DelegatedManagerID;
                return_value.fromDate = return_delegated.FromDate;
                return_value.toDate = return_delegated.ToDate;
                return_value.isActive = return_delegated.isActive;
                return_value.userID = return_delegated.UserID;
                return_value.user = current_delegatedUser;
                return_value.users = return_users;



                return return_value;



            }
            catch (Exception ex) { return null; }


        }

        [HttpPost("{startDate}/{endDate}/{userId}")]
        [Route("AssignDelegate/{startDate}/{endDate}/{userId}")]
        public DelegatedManager AssignDelegate(DateTime startDate, DateTime endDate, int userId)
        {
            DelegatedManager dm = new DelegatedManager();
            dm.DelegatedManagerID = 2;
            dm.FromDate = startDate;
            dm.ToDate = endDate;
            context123.DelegatedManager.Add(dm);
            context123.SaveChanges();
            return dm;
        }

        [HttpPost]
        [Route("SaveDelegatedManager")]
        public String SaveDelegatedManager(DelegatedManager delegatedManager)
        {
            try
            {
                if (delegatedManager.FromDate.Date == DateTime.Now.Date)
                {
                    delegatedManager.isActive = true;
                    User user = context123.User.Where(x => x.UserID == delegatedManager.UserID).FirstOrDefault();
                    if (user != null)
                    {
                        user.Role = "dept_delegate";

                    }
                }
                context123.DelegatedManager.Add(delegatedManager);
                context123.SaveChanges();

                //Sending Email
                User toUser = context123.User.Where(x => x.UserID == delegatedManager.UserID).FirstOrDefault();
                EmailController.SendEmail(toUser.Email,toUser.FirstName+" "+toUser.LastName, "DelegateToEmp");

                return "Success";
            }
            catch (Exception ex)
            {
                return "fail";
            }
        }

        [HttpGet("{userId}/{fromDate}/{toDate}")]
        [Route("SaveDelegatedManagerMB/{userId}/{fromDate}/{toDate}")]
        public dynamic SaveDelegatedManagerMB(int userId, string fromDate, string toDate)
        {
            try
            {
                DateTime fTime = DateTime.ParseExact(fromDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime ttime = DateTime.ParseExact(toDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                User user = context123.User.Where(x => x.UserID == userId).FirstOrDefault();
               // user.DelegatedManager = new DelegatedManager();
                DelegatedManager dm = new DelegatedManager();
                dm.UserID = userId;
                dm.FromDate = fTime;
                dm.ToDate = ttime;
                if (fTime.Date == DateTime.Now.Date)
                {
                    dm.isActive = true;
                    user.Role = "dept_delegate";
                }
                else dm.isActive = false;
                context123.DelegatedManager.Add(dm);
                
                 context123.SaveChanges();

                //Sending Email
                EmailController.SendEmail(user.Email, user.FirstName + " " + user.LastName, "DelegateToEmp");

                DelegatedManager return_delegated = new DelegatedManager();
                User current_delegatedUser = new User();
                List<int> users = context123.User.Where(x => x.DepartmentID == user.DepartmentID).Select(c => c.UserID).ToList();
                List<DelegatedManager> delegated_list = context123.DelegatedManager.Where(x => users.Contains(x.UserID) && x.FromDate >= DateTime.Now.Date).OrderBy(x => x.FromDate).ToList();  //current active delegate
                if (delegated_list.Count > 0)
                {
                    DelegatedManager current_active_delegate = delegated_list.Where(x => x.isActive == true).FirstOrDefault();

                    if (current_active_delegate != null)
                    {
                        return_delegated = current_active_delegate;

                    }
                    else
                    {   //no current active delegate
                        return_delegated = delegated_list[0];

                    }
                    if (return_delegated.User != null)
                    {
                        current_delegatedUser = new User { UserID = return_delegated.User.UserID, FirstName = return_delegated.User.FirstName, LastName = return_delegated.User.LastName };
                    }
                }
                List<User> Users = context123.User.Where(x => x.DepartmentID == user.DepartmentID && x.ReportToID!=null).ToList();
                List<User> return_users = new List<User>();
                foreach (User u in Users)
                {
                    User newUser = new User()
                    {
                        UserID = u.UserID,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    };
                    return_users.Add(newUser);
                }


                dynamic return_value = new System.Dynamic.ExpandoObject();
                return_value.delegatedManagerID = return_delegated.DelegatedManagerID;
                return_value.fromDate = return_delegated.FromDate;
                return_value.toDate = return_delegated.ToDate;
                return_value.isActive = return_delegated.isActive;
                return_value.userID = return_delegated.UserID;
                return_value.user = current_delegatedUser;
                return_value.users = return_users;

                return_value.users = new List<User>();

                return return_value;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        [HttpGet("{id}")]
        public DelegatedManager GetDelegatedManager(int id)
        {
            DelegatedManager delegatedManager = context123.DelegatedManager.First(c => c.DelegatedManagerID == id);

            return delegatedManager;
        }

        [HttpDelete("{id}")]
        [Route("DeleteDelegate/{id}")]
        public string DeleteDelegate(int id)
        {
            try
            {
                User user = context123.User.First(c => c.DelegatedManager.DelegatedManagerID == id);
                if (user != null)
                {
                    user.DelegatedManager = new DelegatedManager();
                    user.Role = "dept_employee";
                }
                //context123.SaveChanges();
                DelegatedManager delegated = context123.DelegatedManager.First(c => c.DelegatedManagerID == id);
                context123.DelegatedManager.Remove(delegated);
                context123.SaveChanges();


                return "Success";
            }
            catch (Exception ex)
            {
                return "Fail";
            }
        }

        [HttpGet("{DMid}")]
        [Route("DeleteDelegateMB/{DMid}")]
        public dynamic DeleteDelegateMB(int DMid)
        {
            try
            {
                DelegatedManager delegated = context123.DelegatedManager.First(c => c.DelegatedManagerID == DMid);
                if (delegated != null)
                {

                    User user = context123.User.First(c => c.UserID == delegated.UserID);
                    if (user != null)
                    {
                        if(delegated.isActive==true)
                        user.Role = "dept_employee";
                    }
                    //context123.SaveChanges();

                    context123.DelegatedManager.Remove(delegated);
                    context123.SaveChanges();

                    User Ruser = context123.User.Where(x => x.UserID == delegated.UserID).FirstOrDefault();

                    dynamic returnData = getCurrentDelegate(Ruser.DepartmentID);
                    return returnData;
                }
                else return null;
              
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{userId}/{fromDate}/{toDate}")]
        [Route("isActiveDelegateByUserID/{userId}/{fromDate}/{toDate}")]
        public bool isActiveDelegateByUserID(int userId, string fromDate, string toDate)
        {
            DateTime fTime = DateTime.ParseExact(fromDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime ttime = DateTime.ParseExact(toDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            List<DelegatedManager> delegated = context123.DelegatedManager.Where(x =>
             x.UserID == userId
            && ((x.FromDate >= fTime.Date && x.ToDate <= ttime.Date) ||
            (x.FromDate <= fTime.Date && x.ToDate >= fTime.Date))).ToList();
            if (delegated.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        [HttpGet("{depid}/{fromDate}/{toDate}")]
        [Route("isActiveDelegateByDateNDepID/{depid}/{fromDate}/{toDate}")]
        public bool isActiveDelegateByDateNDepID(int depid, string fromDate, string toDate)
        {
            DateTime fTime = DateTime.ParseExact(fromDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime ttime = DateTime.ParseExact(toDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            List<int> users = context123.User.Where(x => x.DepartmentID == depid).Select(c => c.UserID).ToList();
            List<DelegatedManager> delegated = context123.DelegatedManager.Where(x =>
            users.Contains(x.UserID)
            && ((x.FromDate >= fTime.Date && x.ToDate <= ttime.Date) ||
            (x.FromDate <= fTime.Date && x.ToDate >= fTime.Date))
            /* && x.isActive == true*/).ToList();
            if (delegated.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
