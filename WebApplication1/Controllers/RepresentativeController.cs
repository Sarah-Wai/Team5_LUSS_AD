using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace LUSS_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class RepresentativeController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<RepresentativeController> _logger;
        public RepresentativeController(ILogger<RepresentativeController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        //SAVE AND UPDATE REPRESENTAIVE USER
        [HttpGet("{id}/{status}/{depID}")]
        [Route("SaveRepresentative/{id}/{status}/{depID}")]
        public string SaveRepresentative(int id,bool status,int depID)
        {
            List<User> Users = context123.User.Where(x => x.DepartmentID == depID && x.UserID!=id && x.IsRepresentative==true).ToList();   
            foreach(User u in Users)
            { 
                u.IsRepresentative = false;   // delegate other representative 
            }

            User representativeUser = context123.User.First(c => c.UserID == id);
            
            if (representativeUser != null)
            {
               
                representativeUser.IsRepresentative = status;
                if (status) representativeUser.Role = "dept_rep";
                else representativeUser.Role = "dept_employee";
                representativeUser.DelegatedManager = null;
                representativeUser.RequestedBy = null;
                representativeUser.RequestMade = null;
                representativeUser.RequestModified = null;
                representativeUser.RequestModified = null;

            }
            
            try
            {
               context123.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return "Success";
        }

          

    }
}
