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
        public ActionResult ChangePin(String oldpin,String newpin,String confirmpin)
        {
            if (Session["PIN"].ToString().Equals(oldpin))
            {
                if (oldpin.Equals(newpin))
                {
                    TempData["message"] = "New PIN must not equal to old";
                }
                else {
                    if (newpin.Equals(confirmpin))
                    {
                        TempData["message"] = connection.UpdatePIN(newpin, Session["empID"].ToString());
                    }
                    else
                    {
                        TempData["message"] = "PIN does not match";
                    }
                }
            }
            else {
                TempData["message"] = "Old PIN is incorrect";
            }
            return View();
        }
        [HttpGet]
        public ActionResult ChangePin()
        {
            return View();
        }
    }
}