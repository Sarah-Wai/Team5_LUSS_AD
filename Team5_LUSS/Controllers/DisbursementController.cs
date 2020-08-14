using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Team5_LUSS.Controllers
{
    public class DisbursementController : Controller
    {
        public IActionResult Index()
        {
            return View();
            //return View("Disbursement_Form_View");
            //return View("Retrieval_Form");
            //return View("Disbursement_Form_Create");
        }
    }
}