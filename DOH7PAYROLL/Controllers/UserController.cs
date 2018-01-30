using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOH7PAYROLL.Repo;
using DOH7PAYROLL.Models;

namespace DOH7PAYROLL.Controllers
{
    public class UserController : Controller
    {
        DatabaseConnect connection = new DatabaseConnect();
        // GET: User
        [HttpPost]
        public ActionResult ChangePin(String pin,String userid)
        {
            TempData["message"] = connection.UpdatePIN(pin,userid);
            return View();
        }
        [HttpGet]
        public ActionResult ChangePin()
        {
            return View();
        }
    }
}