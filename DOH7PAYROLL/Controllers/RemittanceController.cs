using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOH7PAYROLL.Repo;
using DOH7PAYROLL.Models;


namespace DOH7PAYROLL.Controllers
{
    public class RemittanceController : Controller
    {
        DatabaseConnect connection = new DatabaseConnect();
        // GET: Remittance
        public ActionResult Pagibig()
        {
            if (Session["empID"] != null)
            {
                Session["RemitType"] = "Pagibig";
                String id = Request["id"];
                String search = Request["search"];
                String submit = Request["submit"];
                if (id == null)
                {
                    id = "3";
                }
                if (search == null)
                {
                    search = "";
                }
                if (submit != null)
                {
                    if (submit.Equals("Refresh"))
                    {
                        search = "";
                        id = "3";
                    }
                }

                ViewBag.List = connection.GetRemittance("pagibig_remittance",id, search);
                ViewBag.Prev = DatabaseConnect.start;
                ViewBag.Next = DatabaseConnect.end;
                ViewBag.Max = int.Parse(DatabaseConnect.max_size);
                ViewBag.Search = search;
                return View();
            }
            else {
                return RedirectToAction("Index", "Login");
            } 
        }

        public ActionResult Phic()
        {
            if (Session["empID"] != null)
            {
                Session["RemitType"] = "Phic";
                String id = Request["id"];
                String search = Request["search"];
                String submit = Request["submit"];
                if (id == null)
                {
                    id = "3";
                }
                if (search == null)
                {
                    search = "";
                }
                if (submit != null)
                {
                    if (submit.Equals("Refresh"))
                    {
                        search = "";
                        id = "3";
                    }
                }

                ViewBag.List = connection.GetRemittance("phic_remittance", id, search);
                ViewBag.Prev = DatabaseConnect.start;
                ViewBag.Next = DatabaseConnect.end;
                ViewBag.Max = int.Parse(DatabaseConnect.max_size);
                ViewBag.Search = search;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Excess()
        {
            if (Session["empID"] != null)
            {
                Session["RemitType"] = "Excess";
                String id = Request["id"];
                String search = Request["search"];
                String submit = Request["submit"];
                if (id == null)
                {
                    id = "3";
                }
                if (search == null)
                {
                    search = "";
                }
                if (submit != null)
                {
                    if (submit.Equals("Refresh"))
                    {
                        search = "";
                        id = "3";
                    }
                }

                ViewBag.List = connection.GetRemittance("excess_remittance", id, search);
                ViewBag.Prev = DatabaseConnect.start;
                ViewBag.Next = DatabaseConnect.end;
                ViewBag.Max = int.Parse(DatabaseConnect.max_size);
                ViewBag.Search = search;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult Gsis()
        {
            if (Session["empID"] != null)
            {
                Session["RemitType"] = "Gsis";
                String id = Request["id"];
                String search = Request["search"];
                String submit = Request["submit"];
                if (id == null)
                {
                    id = "3";
                }
                if (search == null)
                {
                    search = "";
                }
                if (submit != null)
                {
                    if (submit.Equals("Refresh"))
                    {
                        search = "";
                        id = "3";
                    }
                }

                ViewBag.List = connection.GetRemittance("gsis_remittance", id, search);
                ViewBag.Prev = DatabaseConnect.start;
                ViewBag.Next = DatabaseConnect.end;
                ViewBag.Max = int.Parse(DatabaseConnect.max_size);
                ViewBag.Search = search;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Disallowance()
        {
            if (Session["empID"] != null)
            {
                Session["RemitType"] = "Disallowance";
                String id = Request["id"];
                String search = Request["search"];
                String submit = Request["submit"];
                if (id == null)
                {
                    id = "3";
                }
                if (search == null)
                {
                    search = "";
                }
                if (submit != null)
                {
                    if (submit.Equals("Refresh"))
                    {
                        search = "";
                        id = "3";
                    }
                }

                ViewBag.List = connection.GetRemittance("disallowance_remittance", id, search);
                ViewBag.Prev = DatabaseConnect.start;
                ViewBag.Next = DatabaseConnect.end;
                ViewBag.Max = int.Parse(DatabaseConnect.max_size);
                ViewBag.Search = search;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Coop()
        {
            if (Session["empID"] != null)
            {
                Session["RemitType"] = "Coop";
                String id = Request["id"];
                String search = Request["search"];
                String submit = Request["submit"];
                if (id == null)
                {
                    id = "3";
                }
                if (search == null)
                {
                    search = "";
                }
                if (submit != null)
                {
                    if (submit.Equals("Refresh"))
                    {
                        search = "";
                        id = "3";
                    }
                }

                ViewBag.List = connection.GetRemittance("coop_remittance", id, search);
                ViewBag.Prev = DatabaseConnect.start;
                ViewBag.Next = DatabaseConnect.end;
                ViewBag.Max = int.Parse(DatabaseConnect.max_size);
                ViewBag.Search = search;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // GET: Remittance/Create
        public ActionResult InsertRemittance()
        {
            String empID = Request["remit_empID"];
            String max = Request["remit_maxCount"];
            String count = Request["remit_count"];
            String amount = Request["remit_amount"];
            String submit = Request["remit_submit"];
            Remittance remmitance = new Remittance("0", empID, max, count, amount);
            switch (Session["RemitType"].ToString()) {
                case "Pagibig":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = connection.InsertRemittance("pagibig_remittance", remmitance);
                    }
                    else {
                        TempData["message"] = connection.UpdateRemittance("pagibig_remittance", remmitance);
                    }

                    return RedirectToAction("Pagibig");
                case "Coop":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = connection.InsertRemittance("coop_remittance", remmitance);
                    }
                    else
                    {
                        TempData["message"] = connection.UpdateRemittance("coop_remittance", remmitance);
                    }
                    return RedirectToAction("Coop");
                case "Excess":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = connection.InsertRemittance("excess_remittance", remmitance);
                    }
                    else
                    {
                        TempData["message"] = connection.UpdateRemittance("excess_remittance", remmitance);
                    }
                    return RedirectToAction("Excess");
                case "Phic":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = connection.InsertRemittance("phic_remittance", remmitance);
                    }
                    else
                    {
                        TempData["message"] = connection.UpdateRemittance("phic_remittance", remmitance);
                    }
                    return RedirectToAction("Phic");
                case "Disallowance":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = connection.InsertRemittance("disallowance_remittance", remmitance);
                    }
                    else
                    {
                        TempData["message"] = connection.UpdateRemittance("disallowance_remittance", remmitance);
                    }
                    return RedirectToAction("Disallowance");
                case "Gsis":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = connection.InsertRemittance("gsis_remittance", remmitance);
                    }
                    else
                    {
                        TempData["message"] = connection.UpdateRemittance("gsis_remittance", remmitance);
                    }
                    return RedirectToAction("Gsis");
            }
            return RedirectToAction("Pagibig");
        }

        public ActionResult DeleteRemittance()
        {
            String empID = Request["empID"];
            switch (Session["RemitType"].ToString())
            {
                case "Pagibig":
                    TempData["message"] = connection.DeleteRemittance("pagibig_remittance", empID);
                    return RedirectToAction("Pagibig");
                case "Coop":
                    TempData["message"] = connection.DeleteRemittance("coop_remittance", empID);
                    return RedirectToAction("Coop");
                case "Excess":
                    TempData["message"] = connection.DeleteRemittance("excess_remittance", empID);
                    return RedirectToAction("Excess");
                case "Phic":
                    TempData["message"] = connection.DeleteRemittance("phic_remittance", empID);
                    return RedirectToAction("Phic");
                case "Disallowance":
                    TempData["message"] = connection.DeleteRemittance("disallowance_remittance", empID);
                    return RedirectToAction("Disallowance");
                case "Gsis":
                    TempData["message"] = connection.DeleteRemittance("gsis_remittance", empID);
                    return RedirectToAction("Gsis");
            }
            return RedirectToAction("Pagibig");
        }
    }
}
