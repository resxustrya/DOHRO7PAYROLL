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
        // GET: Remittance
        public ActionResult Pagibig()
        {
            if (Session["empID"] != null)
            {
                Session["RemitType"] = "Pagibig";
                String id = Request["id"];
                String search = Request["search"];
                String submit = Request["submit"];
                String start = Request["start"];
                String next = Request["next"];
                String max = Request["max"];

                if (start == null)
                {
                    start = "0";
                }
                if (next == null)
                {
                    next = "0";
                }
                if (max == null)
                {
                    max = "0";
                }
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
                        start = "0";
                        next = "0";
                        max = "0";
                    }
                }
                DatabaseConnect.start = int.Parse(start);
                DatabaseConnect.end = int.Parse(next);
                DatabaseConnect.max_size = max;
                ViewBag.List = DatabaseConnect.Instance.GetRemittance("pagibig_remittance",id, search);
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
                String start = Request["start"];
                String next = Request["next"];
                String max = Request["max"];

                if (start == null)
                {
                    start = "0";
                }
                if (next == null)
                {
                    next = "0";
                }
                if (max == null)
                {
                    max = "0";
                }
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
                        start = "0";
                        next = "0";
                        max = "0";
                    }
                }
                DatabaseConnect.start = int.Parse(start);
                DatabaseConnect.end = int.Parse(next);
                DatabaseConnect.max_size = max;
                ViewBag.List = DatabaseConnect.Instance.GetRemittance("phic_remittance", id, search);
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
                String start = Request["start"];
                String next = Request["next"];
                String max = Request["max"];

                if (start == null)
                {
                    start = "0";
                }
                if (next == null)
                {
                    next = "0";
                }
                if (max == null)
                {
                    max = "0";
                }
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
                        start = "0";
                        next = "0";
                        max = "0";
                    }
                }
                DatabaseConnect.start = int.Parse(start);
                DatabaseConnect.end = int.Parse(next);
                DatabaseConnect.max_size = max;
                ViewBag.List = DatabaseConnect.Instance.GetRemittance("excess_remittance", id, search);
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
                String start = Request["start"];
                String next = Request["next"];
                String max = Request["max"];

                if (start == null)
                {
                    start = "0";
                }
                if (next == null)
                {
                    next = "0";
                }
                if (max == null)
                {
                    max = "0";
                }
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
                        start = "0";
                        next = "0";
                        max = "0";
                    }
                }
                DatabaseConnect.start = int.Parse(start);
                DatabaseConnect.end = int.Parse(next);
                DatabaseConnect.max_size = max;
                ViewBag.List = DatabaseConnect.Instance.GetRemittance("gsis_remittance", id, search);
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
                String start = Request["start"];
                String next = Request["next"];
                String max = Request["max"];

                if (start == null)
                {
                    start = "0";
                }
                if (next == null)
                {
                    next = "0";
                }
                if (max == null)
                {
                    max = "0";
                }
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
                        start = "0";
                        next = "0";
                        max = "0";
                    }
                }
                DatabaseConnect.start = int.Parse(start);
                DatabaseConnect.end = int.Parse(next);
                DatabaseConnect.max_size = max;
                ViewBag.List = DatabaseConnect.Instance.GetRemittance("disallowance_remittance", id, search);
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
                String start = Request["start"];
                String next = Request["next"];
                String max = Request["max"];

                if (start == null)
                {
                    start = "0";
                }
                if (next == null)
                {
                    next = "0";
                }
                if (max == null)
                {
                    max = "0";
                }
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
                        start = "0";
                        next = "0";
                        max = "0";
                    }
                }
                DatabaseConnect.start = int.Parse(start);
                DatabaseConnect.end = int.Parse(next);
                DatabaseConnect.max_size = max;
                ViewBag.List = DatabaseConnect.Instance.GetRemittance("coop_remittance", id, search);
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
                        TempData["message"] = DatabaseConnect.Instance.InsertRemittance("pagibig_remittance", remmitance);
                    }
                    else {
                        TempData["message"] = DatabaseConnect.Instance.UpdateRemittance("pagibig_remittance", remmitance);
                    }

                    return RedirectToAction("Pagibig");
                case "Coop":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = DatabaseConnect.Instance.InsertRemittance("coop_remittance", remmitance);
                    }
                    else
                    {
                        TempData["message"] = DatabaseConnect.Instance.UpdateRemittance("coop_remittance", remmitance);
                    }
                    return RedirectToAction("Coop");
                case "Excess":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = DatabaseConnect.Instance.InsertRemittance("excess_remittance", remmitance);
                    }
                    else
                    {
                        TempData["message"] = DatabaseConnect.Instance.UpdateRemittance("excess_remittance", remmitance);
                    }
                    return RedirectToAction("Excess");
                case "Phic":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = DatabaseConnect.Instance.InsertRemittance("phic_remittance", remmitance);
                    }
                    else
                    {
                        TempData["message"] = DatabaseConnect.Instance.UpdateRemittance("phic_remittance", remmitance);
                    }
                    return RedirectToAction("Phic");
                case "Disallowance":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = DatabaseConnect.Instance.InsertRemittance("disallowance_remittance", remmitance);
                    }
                    else
                    {
                        TempData["message"] = DatabaseConnect.Instance.UpdateRemittance("disallowance_remittance", remmitance);
                    }
                    return RedirectToAction("Disallowance");
                case "Gsis":
                    if (submit.Equals("0"))
                    {
                        TempData["message"] = DatabaseConnect.Instance.InsertRemittance("gsis_remittance", remmitance);
                    }
                    else
                    {
                        TempData["message"] = DatabaseConnect.Instance.UpdateRemittance("gsis_remittance", remmitance);
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
                    TempData["message"] = DatabaseConnect.Instance.DeleteRemittance("pagibig_remittance", empID);
                    return RedirectToAction("Pagibig");
                case "Coop":
                    TempData["message"] = DatabaseConnect.Instance.DeleteRemittance("coop_remittance", empID);
                    return RedirectToAction("Coop");
                case "Excess":
                    TempData["message"] = DatabaseConnect.Instance.DeleteRemittance("excess_remittance", empID);
                    return RedirectToAction("Excess");
                case "Phic":
                    TempData["message"] = DatabaseConnect.Instance.DeleteRemittance("phic_remittance", empID);
                    return RedirectToAction("Phic");
                case "Disallowance":
                    TempData["message"] = DatabaseConnect.Instance.DeleteRemittance("disallowance_remittance", empID);
                    return RedirectToAction("Disallowance");
                case "Gsis":
                    TempData["message"] = DatabaseConnect.Instance.DeleteRemittance("gsis_remittance", empID);
                    return RedirectToAction("Gsis");
            }
            return RedirectToAction("Pagibig");
        }
    }
}
