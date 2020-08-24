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
            try { 
                DelegatedManager delegatedManager = context123.DelegatedManager.First(c => c.UserID == id);
                return delegatedManager;

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

        [HttpGet("{id}")]
        public DelegatedManager GetDelegatedManager(int id)
        {
            DelegatedManager delegatedManager = context123.DelegatedManager.First(c => c.DelegatedManagerID == id);

            return delegatedManager;
        }

        [HttpDelete("{id}")]
        [Route("DeleteDelegate/{id}")]
        public void DeleteDelegate(int id)
        {
            User user = context123.User.First(c => c.DelegatedManager.DelegatedManagerID == id);
            if (user != null) {
                user.DelegatedManager = new DelegatedManager();
            }
            //context123.SaveChanges();
            DelegatedManager delegated = context123.DelegatedManager.First(c => c.DelegatedManagerID == id);
            context123.DelegatedManager.Remove(delegated);
            context123.SaveChanges();
        }

        [HttpDelete("{id}")]
        [Route("isActiveDelegateByUserID/{id}")]
        public bool isActiveDelegateByUserID(int id)
        {
         
            List<DelegatedManager> delegated = context123.DelegatedManager.Where(x => x.UserID == id && (x.FromDate.Date<=DateTime.Now && x.FromDate.Date>=DateTime.Now) && x.isActive==false ).ToList();
            if (delegated.Count>0)
            {
                foreach (DelegatedManager d in delegated) {
                    d.isActive = true;
                }
                context123.SaveChanges();
            }
            DelegatedManager activeDelegatedManager = context123.DelegatedManager.First(c => c.UserID == id && c.isActive == true);
            if (activeDelegatedManager != null)
                return true;
            else return false;
        
        }

        [HttpGet("{depid}/{fromDate}/{toDate}")]
        [Route("isActiveDelegateByDateNDepID/{depid}/{fromDate}/{toDate}")]
        public bool isActiveDelegateByDateNDepID(int depid,string fromDate,string toDate)
        {
            DateTime fTime = DateTime.ParseExact(fromDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime ttime = DateTime.ParseExact(toDate.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            List<int> users = context123.User.Where(x => x.DepartmentID == depid).Select(c=>c.UserID).ToList();
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
