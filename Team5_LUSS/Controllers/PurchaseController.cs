using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Team5_LUSS.Controllers
{
    public class PurchaseController : Controller
    {
        public IActionResult Index()
        {
            //return View();
            //return View("PO_LowStock");
            //return View("PO_History");
            //return View("PO_Receive");
            //return View("PO_Create");
            //return View("PO_Create_Bulk");
            return View("PO_Create_Low");
        }
    }
}