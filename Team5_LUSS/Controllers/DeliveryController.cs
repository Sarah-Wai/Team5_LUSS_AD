using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_LUSS.Controllers
{
    public class DeliveryController : Controller
    {

        public IActionResult ByRequests()
        {
            return View("ConfirmDelivery");
            //return View("Disbursement_Form_Create");
        }



    }
}
