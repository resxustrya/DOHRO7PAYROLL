using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOH7PAYROLL.Controllers
{
    public class PayrollController : Controller
    {
        // GET: Payroll
        public ActionResult Job_Order()
        {
            return View();
        }

        public ActionResult Regular()
        {
            return View();
        }
    }
}