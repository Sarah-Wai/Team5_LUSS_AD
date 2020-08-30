using LUSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LUSS_API.DB
{
    public class AddDelegate
    {

        public static DelegatedManager getDelegate()
        {
            DelegatedManager dm1 = new DelegatedManager
            {
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddDays(2),
                isActive = true,
                UserID = 9
            };

            return dm1;
            
        }
    }
}
