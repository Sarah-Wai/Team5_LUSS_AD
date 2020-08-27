using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Globalization;

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
                List<int> users = context123.User.Where(x => x.DepartmentID == depID).Select(c => c.UserID).ToList();
                DelegatedManager delegated = context123.DelegatedManager.Where(x => users.Contains(x.UserID) &&
                x.isActive == true).FirstOrDefault();
                DelegatedManager current_delegated = new DelegatedManager();
                User current_delegatedUser = new User();
                if (delegated != null)
                {
                    current_delegated = delegated;
                    current_delegatedUser = new User { UserID = current_delegated.User.UserID, FirstName = current_delegated.User.FirstName, LastName = current_delegated.User.LastName };
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
                return_value.delegatedManagerID = current_delegated.DelegatedManagerID;
                return_value.fromDate = current_delegated.FromDate;
                return_value.toDate = current_delegated.ToDate;
                return_value.isActive = current_delegated.isActive;
                return_value.userID = current_delegated.UserID;
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
        public async Task<String> SaveDelegatedManager(DelegatedManager delegatedManager)
        {
            context123.DelegatedManager.Add(delegatedManager);
            await context123.SaveChangesAsync();

            return "Success";
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
                if (fTime.Date == DateTime.Now.Date) dm.isActive = true;
                else dm.isActive = false;
                context123.DelegatedManager.Add(dm);
                
                 context123.SaveChanges();
              

                List<int> users = context123.User.Where(x => x.DepartmentID == user.DepartmentID).Select(c => c.UserID).ToList();
                DelegatedManager delegated = context123.DelegatedManager.Where(x => users.Contains(x.UserID) &&
                x.isActive == true).FirstOrDefault();
                DelegatedManager current_delegated = new DelegatedManager();
                User current_delegatedUser = new User();
                if (delegated != null)
                {
                    current_delegated = delegated;
                    current_delegatedUser = new User { UserID = current_delegated.User.UserID, FirstName = current_delegated.User.FirstName, LastName = current_delegated.User.LastName };
                }
            

                dynamic return_value = new System.Dynamic.ExpandoObject();
                return_value.delegatedManagerID = current_delegated.DelegatedManagerID;
                return_value.fromDate = current_delegated.FromDate;
                return_value.toDate = current_delegated.ToDate;
                return_value.isActive = current_delegated.isActive;
                return_value.userID = current_delegated.UserID;
                return_value.user = current_delegatedUser;
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

                    //User user = context123.User.First(c => c.UserID == delegated.UserID);
                    //if (user != null)
                    //{

                    //    user.DelegatedManager = new DelegatedManager();
                    //}
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
