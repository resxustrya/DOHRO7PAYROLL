using System;
using System.Web.Mvc;
using System.Web;
using DOH7PAYROLL.Repo;
using DOH7PAYROLL.Models;


namespace DOH7PAYROLL.Controllers
{

    
    public class NoCache : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }

    [NoCache]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class LoginController : Controller
    {
        DatabaseConnect connection = new DatabaseConnect();


        // GET: Login
        [NoCache]
        public ActionResult Index()
        {
            if (Session["empID"] != null)
            {
                if (Session["LoginType"].Equals("0"))
                {
                    return RedirectToAction("Payroll_List", "Payroll");
                }
                else
                {
                    return RedirectToAction("Job_Order", "Payroll");
                }
            }
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return View();
        }
        [NoCache]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Session["empID"] = null;
            return RedirectToAction("Index","Login");
        }

        [HttpPost]
        public ActionResult Index(String username, String password)
        {
                Employee employee = connection.Login(username);
            if (employee != null)
            {
                if (employee.JobType.Equals("") || employee.JobType.Equals("Inactive"))
                {
                    TempData["message"] = "User not activated.";
                    return View();
                }
                else {
                    Session["empID"] = employee.PersonnelID;
                    Session["Name"] = employee.Firstname + " " + employee.MiddleName + " " + employee.Lastname;
                    Session["Section"] = employee.Section;
                    if (password.Equals("123"))
                    {
                        Session["LoginType"] = "0";
                        return RedirectToAction("Payroll_List", "Payroll");
                    }
                    else if (password.Equals("payroll_123"))
                    {
                        Session["LoginType"] = "1";
                        return RedirectToAction("Job_Order", "Payroll");
                    }
                    else {
                        TempData["message"] = "Incorrect password";
                        return View();
                    }
                }
            }
            TempData["message"] = "User ID don't exists.";
            return View();
        }
    }
}
