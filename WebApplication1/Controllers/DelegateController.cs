using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

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
            DelegatedManager delegated = context123.DelegatedManager.First(c => c.DelegatedManagerID == id);
            context123.DelegatedManager.Remove(delegated);
            context123.SaveChanges();
        }
    }
}
