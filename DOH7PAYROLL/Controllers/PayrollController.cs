using System;
using System.Text;
using System.Web.Mvc;
using DOH7PAYROLL.Repo;
using DOH7PAYROLL.Models;
using System.Web.Routing;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text;



namespace DOH7PAYROLL.Controllers
{
    public class PayrollController : Controller
    {

        DatabaseConnect connection = new DatabaseConnect();

        // GET: Payroll
        public ActionResult Job_Order()
        {
            if (Session["empID"] != null)
            {
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

                ViewBag.List = connection.GetEmployee(id, search, "Job Order");
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

        public ActionResult Regular()
        {
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

            ViewBag.List = connection.GetEmployee(id, search, "Permanent");
            ViewBag.Prev = DatabaseConnect.start;
            ViewBag.Next = DatabaseConnect.end;
            ViewBag.Max = int.Parse(DatabaseConnect.max_size);
            ViewBag.Search = search;
            return View();
        }

        public ActionResult Payroll_Redirect() {
            Session["empID"] = Request["empID"];
            //

            Session["Salary"] = connection.GetLoans(Session["empID"].ToString());
            Session["Coop"] = connection.GetAmount("coop_remittance",Session["empID"].ToString());
            Session["Disallowance"] = connection.GetAmount("disallowance_remittance", Session["empID"].ToString());
            Session["PagIbig"] = connection.GetAmount("pagibig_remittance", Session["empID"].ToString());
            Session["Phic"] = connection.GetAmount("phic_remittance", Session["empID"].ToString());
            Session["Gsis"] = connection.GetAmount("gsis_remittance", Session["empID"].ToString());
            Session["Excess"] = connection.GetAmount("excess_remittance", Session["empID"].ToString());
            return RedirectToAction("Payroll_List");
        }
        public ActionResult Payroll_List()
        {
            if (Session["empID"] != null)
            {
                String empID = Session["empID"].ToString();
                String id = Request["id"];
                String search = Request["search"];
                String submit = Request["submit"];
                if (empID == null)
                {
                    empID = "3";
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
                    }
                }
                ViewBag.List = connection.GetPayroll(empID, id, search);
                ViewBag.Prev = DatabaseConnect.start;
                ViewBag.Next = DatabaseConnect.end;
                ViewBag.Max = int.Parse(DatabaseConnect.max_size);
                ViewBag.Search = search;
                ViewBag.Id = empID;
                return View();
            }
            else {
                return RedirectToAction("Index", "Login");
            } 
        }

        public ActionResult ViewPdf(String pdf) {
            string strAttachment = Server.MapPath(Url.Content("~/public/Pdf/" + pdf));
            return File(strAttachment, "application/pdf");
        }

        public ActionResult Payroll()
        {
            if (Session["empID"] != null)
            {
                String id = Request["id"];
                String search = Request["search"];
                String submit = Request["submit"];

                if (submit == null)
                {
                    submit = "";
                }
                if (search == null)
                {
                    search = "";
                }
                if (id == null)
                {
                    id = "3";
                }
                if (submit.Equals("Refresh"))
                {
                    search = "";
                    id = "3";
                }
                List<PdfFile> list = new List<PdfFile>();
                if (Session["LoginType"].Equals("0"))
                {
                    list = connection.FetchPdf(id, search, Session["empID"].ToString());
                }
                else
                {
                    list = connection.FetchPdf(id, search, "0");
                }
                ViewBag.List = list;
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
        
        [HttpPost]
        public ActionResult Insert()
        {
            String payroll_id = Request["payroll_id"];
            String id = Request["id"];
            String start_date = Request["month_range_value"].Split(' ')[0];
            String end_date = Request["month_range_value"].Split(' ')[1];
            String adjustment = Request["adjustment"];
            String working_days = Request["working_days"];
            String absent_date_list = Request["no_days_absent"];
            String salary = Request["salary"];
            String minutes_late = Request["minutes_late"];
            String coop = Request["coop"];
            String phic = Request["phic"];
            String disallowance = Request["disallowance"];
            String gsis = Request["gsis"];
            String pagibig = Request["pagibig"];
            String excess = Request["excess"];
            String type_request = Request["type_request"];
            String remarks = Request["remarks"];

            Employee employee = new Employee(id,"","","","","","");
            String message = "";
            Payroll payroll = new Payroll(payroll_id, employee,start_date,end_date, adjustment.Replace(",", ""),
                working_days, absent_date_list, salary.Replace(",",""), minutes_late, coop.Replace(",", ""), phic.Replace(",", ""), 
                disallowance.Replace(",", ""), gsis.Replace(",", ""), pagibig.Replace(",", ""), excess.Replace(",", ""),remarks, "");

            if (payroll_id.Equals(""))
            {
                message = connection.Insert(payroll);
            }
            else
                message = connection.Update(payroll);

            TempData["message"] = message;

            return RedirectToAction("Payroll_List");
        }

        public ActionResult DeletePayroll()
        {
            String id = Request["submit"];
            TempData["message"] = connection.DeletePayroll(id);
            return RedirectToAction("Payroll_List");
        }

        public ActionResult DeletePayrollPDF()
        {
            String id = Request["submit"];
            TempData["pdf"] = connection.DeletePayrollPDF(id);
            return RedirectToAction("Payroll");
        }


        [HttpPost]
        public String GetMins(String id,String from, String to,String am_in,String am_out,String pm_in,String pm_out) {
            return connection.GetMins(id, from, to,am_in,am_out,pm_in,pm_out);
            
;        }

        public String ifWeekend(String date) {
            return connection.ifWeekend(date) ?"WEEKEND":"NOT WEEKEND";
        }

        /*
        [HttpGet]
        public FileResult Export()
        {
            DateTime localDate = DateTime.Now;
            int year = localDate.Year;
            int month = localDate.Month;
            int day = localDate.Day;
            int hour = localDate.Hour;
            int mins = localDate.Minute;
            int sec = localDate.Second;
            string filename = "JO_Payroll_" + year + "_" + month + "_" + day + "_" + hour + "_" + mins + "_" + sec+ ".xlsx";
            Employee entities = new Employee();
            DataTable dt = new DataTable("Payroll");
            dt.Columns.AddRange(new DataColumn[17] {
                                            new DataColumn("ID"),
                                            new DataColumn("Firstname"),
                                            new DataColumn("Lastname"),
                                            new DataColumn("Position"),
                                            new DataColumn("Salary"),
                                            new DataColumn("Half_Salary"),
                                            new DataColumn("Absences"),
                                            new DataColumn("Net Amount"),
                                            new DataColumn("Tax 10%"),
                                            new DataColumn("Tax 3%"),
                                            new DataColumn("Coop"),
                                            new DataColumn("Disallowance"),
                                            new DataColumn("Pagibig"),
                                            new DataColumn("PHIC"),
                                            new DataColumn("GSIS"),
                                            new DataColumn("Excess Mobile"),
                                            new DataColumn("Total Amount")});

            List<Employee> list =  connection.GeneratePayroll();

            foreach (var emp in list)
            {
                string ID = emp.PersonnelID;
                string fname = emp.Firstname;
                string lname = emp.Lastname;
                string position = emp.JobType;
                decimal salary = decimal.Parse(emp.Payroll.Salary);
                decimal half_salary = salary / 2;
                int minutes_late = int.Parse(emp.Payroll.MinutesLate);
                int working_days = int.Parse(emp.Payroll.WorkDays);
                decimal per_day = 0;
                decimal absences = 0;
                if (working_days != 0 && salary != 0){
                    per_day = salary / working_days;
                    absences = (minutes_late * (((per_day) / 8) / 60));
                }
               
                decimal net_amount = 0;
                if (working_days != 0){
                    net_amount = (half_salary - absences);
                }
                decimal tax10 = (decimal)0.10;
                decimal tax3 = (decimal)0.03;
                decimal tax_10 = (net_amount * tax10);
                decimal tax_3 = (net_amount * tax3);

                decimal coop = decimal.Parse(emp.Payroll.Coop);
                decimal disallowance = decimal.Parse(emp.Payroll.Disallowance);
                decimal pagibig = decimal.Parse(emp.Payroll.Pagibig);
                decimal phic = decimal.Parse(emp.Payroll.Phic);
                decimal gsis = decimal.Parse(emp.Payroll.Gsis);
                decimal excess = decimal.Parse(emp.Payroll.ExcessMobile);
                decimal total_amount = (net_amount - tax_10 - tax_3 - coop - disallowance - pagibig - phic - gsis - excess);


                dt.Rows.Add(ID,fname,lname,position,salary.ToString("#,##0.00"),half_salary.ToString("#,##0.00"),
                    absences.ToString("#,##0.00"), net_amount.ToString("#,##0.00"), tax_10.ToString("#,##0.00"), tax_3.ToString("#,##0.00"), 
                    coop.ToString("#,##0.00"), disallowance.ToString("#,##0.00"), pagibig.ToString("#,##0.00"),
                    phic.ToString("#,##0.00"), gsis.ToString("#,##0.00"), excess.ToString("#,##0.00"), total_amount.ToString("#,##0.00"));
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
               wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                }
            }
        }
        */
        
        [HttpPost]
        public String CreatePayslip(String id,String start_date,String end_date)
        {
            String message = "";
           
            int month = int.Parse(start_date.Split('/')[0]);
            int day_from = int.Parse(start_date.Split('/')[1]);
            int day_to = int.Parse(end_date.Split('/')[1]);
            int year = int.Parse(end_date.Split('/')[2]);
            
            String title = DatabaseConnect.getMonthName(month) + " " + day_from+ "-" + day_to+ ", " + year;
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            string strPDFFileName = String.Format(id+"_Payslip_" + DatabaseConnect.getMonthName(month) + "_" + day_from+ "_" + day_to+ "_" + year+ ".pdf");

            Document doc = new Document();
            doc.SetMargins(20f, 20f, 20f, 20f);
            doc.SetPageSize(PageSize.LEGAL.Rotate());

            PdfPTable outer = new PdfPTable(1);
            outer.TotalWidth = 200;
            outer.WidthPercentage = 50;

            PdfPTable header_container = new PdfPTable(1);
            header_container.TotalWidth = 200;
            header_container.WidthPercentage = 50;


            PdfPTable header = new PdfPTable(6);

            header.TotalWidth = 200;
            header.WidthPercentage = 50;

            PdfPTable rate_deduction_container = new PdfPTable(1);
            header_container.TotalWidth = 200;
            header_container.WidthPercentage = 20;

            PdfPTable rate_deductions = new PdfPTable(4);
            float[] headers = { 1, 1, 2,2};
            rate_deductions.SetWidths(headers);
            rate_deductions.TotalWidth = 200;
            rate_deductions.WidthPercentage = 70;


            String strAttachment = Server.MapPath(Url.Content("~/public/Pdf/" + strPDFFileName));
            String imageURL = Server.MapPath(Url.Content("~/public/img/logo.png"));
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(strAttachment, FileMode.Create));
            Payroll payroll = connection.GeneratePayslip(id,start_date,end_date);

            if (payroll != null)
            {
                string TIN = payroll.Employee.Tin;
                string section = payroll.Employee.Section;
                string ID = payroll.Employee.PersonnelID;
                string fname = payroll.Employee.Firstname;
                string lname = payroll.Employee.Lastname;
                string mname = payroll.Employee.MiddleName;
                string position = payroll.Employee.JobType;
                string remarks = payroll.Remarks;
                decimal salary = decimal.Parse(payroll.Salary);
                decimal adjustment = decimal.Parse(payroll.Adjustment);
                decimal half_salary = salary / 2;
                if (day_from == 1 && day_to > 17) {
                    half_salary = salary;
                }
                int minutes_late = int.Parse(payroll.MinutesLate);
                int size = payroll.DaysAbsent.Split(',').Length;
                minutes_late += (480 * size);
                int working_days = int.Parse(payroll.WorkDays);
                decimal per_day = 0;
                decimal absences = 0;
                if (working_days != 0 && salary != 0)
                {
                    per_day = salary / working_days;
                    absences = (minutes_late * (((per_day) / 8) / 60));
                }


                decimal net_amount = 0;
                if (working_days != 0)
                {
                    net_amount = (half_salary + adjustment);
                }
                decimal tax10 = (decimal)0.10;
                decimal tax3 = (decimal)0.03;
                decimal tax2 = (decimal)0.02;
                decimal tax_10 = (net_amount * tax10);
                decimal tax_3 = (net_amount * tax3);
                decimal tax_2 = (net_amount * tax2);
                decimal coop = decimal.Parse(payroll.Coop);
                decimal disallowance = decimal.Parse(payroll.Disallowance);
                decimal pagibig = decimal.Parse(payroll.Pagibig);
                decimal phic = decimal.Parse(payroll.Phic);
                decimal gsis = decimal.Parse(payroll.Gsis);
                decimal excess = decimal.Parse(payroll.ExcessMobile);
                decimal deductions = (tax_10 + tax_3 + tax_2 + coop + disallowance + pagibig + phic + gsis + excess + absences);
                decimal total_amount = net_amount - deductions;

                header.AddCell(new PdfPCell(new Phrase("DOH-RO7, Osmena Blvd, Cebu City   |   " + section, new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = PdfPCell.BOTTOM_BORDER,
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                header.AddCell(new PdfPCell(new Phrase(title, new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = PdfPCell.BOTTOM_BORDER,
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });
                header.AddCell(new PdfPCell(new Phrase(lname + ", " + fname + " " + mname + " (" + position+")", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    Colspan = 6,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                //////////
                rate_deductions.AddCell(new PdfPCell(new Phrase("Net Amount:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(net_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("Basic", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });
              
                rate_deductions.AddCell(new PdfPCell(new Phrase("Deductions", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });


                rate_deductions.AddCell(new PdfPCell(new Phrase("Net Amount:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(net_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("Salary", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(salary.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });


                rate_deductions.AddCell(new PdfPCell(new Phrase("Deductions (-):", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(deductions.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                if (day_from == 1 && day_to > 17)
                {
                    rate_deductions.AddCell(new PdfPCell(new Phrase("Whole Month Salary:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                    {
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        VerticalAlignment = Element.ALIGN_RIGHT
                    });

                    rate_deductions.AddCell(new PdfPCell(new Phrase(half_salary.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                    {
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_LEFT
                    });
                }
                else {
                    rate_deductions.AddCell(new PdfPCell(new Phrase("Half-Salary:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                    {
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        VerticalAlignment = Element.ALIGN_RIGHT
                    });

                    rate_deductions.AddCell(new PdfPCell(new Phrase(half_salary.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                    {
                        Border = 0,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_LEFT
                    });
                }
                int count = 0;

                rate_deductions.AddCell(new PdfPCell(new Phrase("Total Amount:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    BackgroundColor = new BaseColor(0, 0, 0),
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    BackgroundColor = new BaseColor(0, 0, 0),
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("Adjustment:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(adjustment.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase("Total Amount:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("Net Amount:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    BackgroundColor = new BaseColor(124, 124, 124),
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(net_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    BackgroundColor = new BaseColor(124, 124, 124),
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase("Deductions:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("Deductions", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(net_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("Tardiness/Absences:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(absences.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("W/TAX 10%:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(tax_10.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("W/TAX 3%:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(tax_3.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("W/TAX 2%:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(tax_2.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                count = connection.GetRemittanceCount("coop_remittance", ID);
                rate_deductions.AddCell(new PdfPCell(new Phrase("Coop(" + count + "):", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(coop.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                count = connection.GetRemittanceCount("disallowance_remittance", ID);
                rate_deductions.AddCell(new PdfPCell(new Phrase("Disallowance(" + count + "):", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(disallowance.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                 count = connection.GetRemittanceCount("pagibig_remittance",ID);
                rate_deductions.AddCell(new PdfPCell(new Phrase("Pag-Ibig("+ count + "):", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(pagibig.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                count = connection.GetRemittanceCount("phic_remittance", ID);
                rate_deductions.AddCell(new PdfPCell(new Phrase("PHIC(" + count + "):", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(phic.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                count = connection.GetRemittanceCount("gsis_remittance", ID);
                rate_deductions.AddCell(new PdfPCell(new Phrase("GSIS(" + count + "):", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(coop.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                count = connection.GetRemittanceCount("excess_remittance", ID);
                rate_deductions.AddCell(new PdfPCell(new Phrase("Excess Mobile(" + count + "):", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(coop.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(0, 0, 0))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase("Spaces:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase(total_amount.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    Border = PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });
                rate_deductions.AddCell(new PdfPCell(new Phrase("Total Deductions:", new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    BackgroundColor = new BaseColor(124, 124, 124),
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_RIGHT
                });

                rate_deductions.AddCell(new PdfPCell(new Phrase(deductions.ToString("#,##0.00"), new Font(Font.FontFamily.HELVETICA, 10, 1, new BaseColor(255, 255, 255))))
                {
                    BackgroundColor = new BaseColor(124, 124, 124),
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_LEFT
                });

                ////

                header_container.AddCell(new PdfPCell(header){
                    Border=0
                });
                rate_deduction_container.AddCell(new PdfPCell(rate_deductions)
                {
                    Border = 0
                });
                outer.AddCell(new PdfPCell(header_container) {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                });
                outer.AddCell(new PdfPCell(rate_deduction_container)
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                });

                doc.Open();
                doc.Add(outer);
                doc.Close();

                message = connection.InsertPDF(strPDFFileName, "0",id,start_date,end_date);
                return message;
            }
            else {
                return "Failed to Generate";
            }
            //return File(strAttachment, "application/pdf");

        }

        public String Testing(String filter_dates)
        {
            String from_date = filter_dates.Split(' ')[0];
            String to_date = filter_dates.Split(' ')[2];

            return from_date + " " + to_date;

        }
        
        public ActionResult CreatePdf(String filter_dates,String selection)
        {
            String message = "";
            int month = int.Parse(filter_dates.Split('/')[0]);
            int day_from = int.Parse(filter_dates.Split('/')[1]);
            int day_to= int.Parse(filter_dates.Split('/')[3]);
            int year = int.Parse(filter_dates.Split('/')[4]);


            String from_date = filter_dates.Split(' ')[0];
            String to_date = filter_dates.Split(' ')[2];
            filter_dates = DatabaseConnect.getMonthName(month) + " " + day_from + "-" + day_to+", "+year;
            
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            string strPDFFileName = String.Format(selection+"_" + DatabaseConnect.getMonthName(month) + "_" + day_from + "_" + day_to + "_" + year + ".pdf");

            Document doc = new Document();
            doc.SetMargins(20f, 20f, 20f, 20f);
            doc.SetPageSize(PageSize.LEGAL.Rotate());

            PdfPTable outer = new PdfPTable(1);
            outer.TotalWidth = 400;
            outer.WidthPercentage = 100;
            outer.SplitRows = true;
            outer.SplitLate = true;

            PdfPTable body = new PdfPTable(20);
            float[] headers = { 5, 5, 5, 6, 7, 7, 7, 7, 7, 5, 5, 5, 5, 5, 5, 5, 5, 5, 7, 6 };

            PdfPTable footer = new PdfPTable(20);
            footer.SetWidths(headers);
            footer.TotalWidth = 400;
            footer.WidthPercentage = 100;

            String strAttachment = Server.MapPath(Url.Content("~/public/Pdf/"+strPDFFileName));
            String imageURL = Server.MapPath(Url.Content("~/public/img/logo.png"));
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(strAttachment,FileMode.Create));
            doc.Open();
            if (selection.Equals("1"))
            {
                body = addBody(body, filter_dates, imageURL, from_date, to_date);
            }
            else {
                body = addBodyCustom(body, filter_dates, imageURL, from_date, to_date,selection);
            }
           
            footer = addFooter(footer,Session["Name"].ToString());
            outer.AddCell(new PdfPCell(body)
            {
                Border=0,
            });
            outer.AddCell(new PdfPCell(footer)
            {
                Border = 0,
            });
            doc.Add(outer);
            doc.Close();

            message = connection.InsertPDF(strPDFFileName,"1","0",from_date, to_date);
            TempData["pdf"] = message;
           // return File(strAttachment, "application/pdf");

            return RedirectToAction("Payroll", new { id = 3, search = ""});
        }

        protected PdfPTable addBodyCustom(PdfPTable tableLayout, String filter_range1,
           String imageURL, String from_date, String to_date,String selection)
        {
            List<Payroll> payroll = connection.GeneratePayroll(from_date, to_date, selection);
            addHeaderCustom(tableLayout, filter_range1, from_date, to_date, imageURL,selection);

            String description = "";
            decimal grand_pagibig = 0;
            int count = 0;
           
            foreach (var item in payroll)
            {
                count++;
                string TIN = item.Employee.Tin;
                string section = item.Employee.Section;
                string ID = item.Employee.PersonnelID;
                string fname = item.Employee.Firstname;
                string lname = item.Employee.Lastname;
                string position = item.Employee.JobType;
                decimal pagibig = decimal.Parse(item.Pagibig);
                switch (selection)
                {
                    case "3":
                        pagibig = decimal.Parse(item.Coop);
                        break;
                    case "4":
                        pagibig = decimal.Parse(item.Phic);
                        break;
                    case "5":
                        pagibig = decimal.Parse(item.Gsis);
                        break;
                    case "6":
                        pagibig = decimal.Parse(item.ExcessMobile);
                        break;
                }
                
                grand_pagibig += pagibig;
                if (description.Equals(""))
                {
                    addSectionCustom(tableLayout, section);
                }
                else
                {
                    if (!section.Equals(description))
                    {
                        addSectionCustom(tableLayout, section);
                    }
                }
                description = section;
                tableLayout.AddCell(new PdfPCell(new Phrase(TIN, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    Colspan = 4,
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                });
                tableLayout.AddCell(new PdfPCell(new Phrase(fname, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    Colspan = 4,
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                });
                tableLayout.AddCell(new PdfPCell(new Phrase(lname, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    Colspan = 4,
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                });
                tableLayout.AddCell(new PdfPCell(new Phrase(position, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    Colspan = 4,
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                });
                tableLayout.AddCell(new PdfPCell(new Phrase(pagibig.ToString("#,##0.00"), new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    Colspan = 4,
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                });
            }
            return tableLayout;
        }

        protected PdfPTable addBody(PdfPTable tableLayout,String filter_range1,
            String imageURL,String from_date,String to_date)
        {
            List<Payroll> payroll= connection.GeneratePayroll(from_date,to_date,"1");
            addHeader(tableLayout, filter_range1, from_date,to_date, imageURL);
            String description = "";
            decimal overall_net = 0;
            decimal grand_overall_net = 0;
            decimal grand_mo_rate = 0;
            decimal grand_half_rate = 0;
            decimal grand_adjustment = 0;
            decimal grand_absences = 0;
            decimal grand_tax_10 = 0;
            decimal grand_tax_3 = 0;
            decimal grand_tax_2 = 0;
            decimal grand_coop = 0;
            decimal grand_disallow = 0;
            decimal grand_pagibig = 0;
            decimal grand_phic = 0;
            decimal grand_gsis = 0;
            decimal grand_excess = 0;
            decimal grand_total_amount = 0;
            int count = 0;
            foreach (var item in payroll)
            {
                count++;
                string TIN = item.Employee.Tin;
                string section = item.Employee.Section;
                string ID = item.Employee.PersonnelID;
                string fname = item.Employee.Firstname;
                string lname = item.Employee.Lastname;
                string position = item.Employee.JobType;
                string remarks = item.Remarks;
                decimal salary = decimal.Parse(item.Salary);
                decimal adjustment = decimal.Parse(item.Adjustment);
                grand_adjustment += adjustment;
                grand_mo_rate += salary;
                decimal half_salary = 0;
                if (int.Parse(from_date.Split('/')[1]) == 1 && int.Parse(to_date.Split('/')[2]) > 18)
                {
                    half_salary = salary;
                }
                else {
                    half_salary = salary / 2;
                }
                
                int minutes_late = int.Parse(item.MinutesLate);
                int size = int.Parse(item.DaysAbsent);
                minutes_late += (480 * size);
                int working_days = int.Parse(item.WorkDays);
                decimal per_day = 0;
                decimal absences = 0;
                if (working_days != 0 && salary != 0)
                {
                    per_day = salary / working_days;
                    absences = (minutes_late * (((per_day) / 8) / 60));
                }
                grand_absences += absences;

                decimal net_amount = 0;
                if (working_days != 0)
                {
                    net_amount = (half_salary - absences + adjustment);
                }
                grand_overall_net += net_amount;
                decimal tax10 = (decimal)0.10;
                decimal tax3 = (decimal)0.03;
                decimal tax2 = (decimal)0.02;
                decimal tax_10 = (net_amount * tax10);
                grand_tax_10 += tax_10;
                decimal tax_3 = (net_amount * tax3);
                grand_tax_3 += tax_3;
                decimal tax_2 = (net_amount * tax2);
                grand_tax_2 += tax_2;
                decimal coop = decimal.Parse(item.Coop);
                grand_coop += coop;
                decimal disallowance = decimal.Parse(item.Disallowance);
                grand_disallow += disallowance;
                decimal pagibig = decimal.Parse(item.Pagibig);
                grand_pagibig += pagibig;
                decimal phic = decimal.Parse(item.Phic);
                grand_phic += phic;
                decimal gsis = decimal.Parse(item.Gsis);
                grand_gsis += gsis;
                decimal excess = decimal.Parse(item.ExcessMobile);
                grand_excess += excess;
                decimal total_amount = net_amount - tax_10 - tax_3 - coop - disallowance - pagibig - phic - gsis - excess;
                grand_total_amount += total_amount;
                if (description.Equals("")) { 
                    addSection(tableLayout, section);
                    overall_net += net_amount;
                }
                else{
                    if (!section.Equals(description))
                    {
                        addOverall(tableLayout, overall_net.ToString("#,##0.00"));
                        addSection(tableLayout, section);
                        overall_net = net_amount;
                    }
                    else {
                        overall_net += net_amount;
                    }
                }
                description = section;
                AddCellToBody(tableLayout, TIN, "left");
                AddCellToBody(tableLayout, fname, "left");
                AddCellToBody(tableLayout, lname, "left");
                AddCellToBody(tableLayout, position, "left");
                AddCellToBody(tableLayout, salary.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, (salary/2).ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, adjustment.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, absences.ToString("#,##0.00"), "ri ght");
                AddCellToBody(tableLayout, net_amount.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, tax_10.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, tax_3.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, tax_2.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, coop.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, disallowance.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, pagibig.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, phic.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, gsis.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, excess.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, total_amount.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, remarks, "right");
            }
            addOverall(tableLayout, overall_net.ToString("#,##0.00"));
            addGrandOverall(tableLayout, grand_mo_rate.ToString("#,##0.00"), grand_half_rate.ToString("#,##0.00"), 
                grand_adjustment.ToString("#,##0.00"),grand_absences.ToString("#,##0.00"), grand_overall_net.ToString("#,##0.00"), grand_tax_10.ToString("#,##0.00"),
                grand_tax_3.ToString("#,##0.00"), grand_tax_2.ToString("#,##0.00"), grand_coop.ToString("#,##0.00"), grand_disallow.ToString("#,##0.00"),
                grand_pagibig.ToString("#,##0.00"), grand_phic.ToString("#,##0.00"), grand_gsis.ToString("#,##0.00"),grand_excess.ToString("#,##0.00"), grand_total_amount.ToString("#,##0.00"));
            return tableLayout;
        }
        protected static void addHeader(PdfPTable tableLayout,String filter_range1, String from_date,String to_date, String imageURL)
        {

            Image jpg = Image.GetInstance(imageURL);
            //Resize image depend upon your need
            jpg.ScaleToFit(50f, 50f);
            //Give space before image
            //Give some space after the image
            jpg.Alignment = Element.ALIGN_LEFT;


            tableLayout.AddCell(new PdfPCell(jpg)
            {
                Colspan = 1,
                Rowspan = 3,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("PAYROLL FOR JOB PERSONNEL", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 19,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("DOH-RO7", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 19,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(filter_range1, new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 19,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("We acknowledge receipt of the sum shown opposite our names as full renumeration for services rendered for the period started:", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 16,
                Border = 0,
                Padding = 3,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("*NO WORK NO PAY POLICY", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 4,
                Border = 0,
                Padding = 3,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            AddCellToHeader(tableLayout, "TIN");
            AddCellToHeader(tableLayout, "Name");
            AddCellToHeader(tableLayout, "Position");
            AddCellToHeader(tableLayout, "MO. RATE");
            AddCellToHeader(tableLayout, "HALF MO.");
            AddCellToHeader(tableLayout, "Adjustment");
            AddCellToHeader(tableLayout, "Tardiness");
            AddCellToHeader(tableLayout, "Net Amount");
            AddCellToHeader(tableLayout, "Deductions");
            AddCellToHeader(tableLayout, "Total Amt.");
            AddCellToHeader(tableLayout, "Remarks");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "Firstname");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "Absences");
            AddCellToHeader(tableLayout, "W/Tax 10%");
            AddCellToHeader(tableLayout, "W/Tax 3%");
            AddCellToHeader(tableLayout, "W/Tax 2%");
            AddCellToHeader(tableLayout, "Coop");
            AddCellToHeader(tableLayout, "Disallow.");
            AddCellToHeader(tableLayout, "Pag-Ibig");
            AddCellToHeader(tableLayout, "PHIC");
            AddCellToHeader(tableLayout, "GSIS");
            AddCellToHeader(tableLayout, "Excess Mobile");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
        }

        protected static void addHeaderCustom(PdfPTable tableLayout, String filter_range1, String from_date, String to_date, String imageURL,String selection)
        {

            Image jpg = Image.GetInstance(imageURL);
            //Resize image depend upon your need
            jpg.ScaleToFit(50f, 50f);
            //Give space before image
            //Give some space after the image
            jpg.Alignment = Element.ALIGN_LEFT;


            tableLayout.AddCell(new PdfPCell(jpg)
            {
                Colspan = 1,
                Rowspan = 3,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("PAYROLL FOR JOB PERSONNEL", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 19,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("DOH-RO7", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 19,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(filter_range1, new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 19,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("We acknowledge receipt of the sum shown opposite our names as full renumeration for services rendered for the period started:", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 16,
                Border = 0,
                Padding = 3,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("*NO WORK NO PAY POLICY", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 4,
                Border = 0,
                Padding = 3,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });



            tableLayout.AddCell(new PdfPCell(new Phrase("TIN", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                BorderWidth = 0.2f,
                Colspan = 4,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Name", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                Colspan = 8,
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("Position", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                Colspan = 4,
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            String title = "";
            switch (selection)
            {
                case "2":
                    title = "Pag-Ibig";
                    break;
                case "3":
                    title = "Coop";
                    break;
                case "4":
                    title = "PHIC";
                    break;
                case "5":
                    title = "GSIS";
                    break;
                case "6":
                    title = "Excess Mobile";
                    break;
            }
            tableLayout.AddCell(new PdfPCell(new Phrase(title, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                Colspan = 4,
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
        }

        private static void addSectionCustom(PdfPTable tableLayout, String section)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                BorderWidth = 0.2f,
                Colspan = 4,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(section, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                BorderWidth = 0.2f,
                Colspan = 8,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                BorderWidth = 0.2f,
                Colspan = 4,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                BorderWidth = 0.2f,
                Colspan = 4,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

        }

        private static void addSection(PdfPTable tableLayout,String section) {
            AddCellToBody(tableLayout, "", "left");
            tableLayout.AddCell(new PdfPCell(new Phrase(section, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                BorderWidth = 0.2f,
                Colspan = 2,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
        }

        private static void addOverall(PdfPTable tableLayout, String overall)
        {
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            tableLayout.AddCell(new PdfPCell(new Phrase(overall, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD,BaseColor.RED))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
        }

        private static void addGrandOverall(PdfPTable tableLayout, String month,String half,String adjustment,String absent,String net
            ,String tax_10,String tax_3,String tax_2,String coop,String disallow,String pagibig,String phic,String gsis,String excess,
            String total)
        {
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            tableLayout.AddCell(new PdfPCell(new Phrase("Grand Total", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                Colspan = 2,
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(month, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(half, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(adjustment, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(absent, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(net, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.RED))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(tax_10, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(tax_3, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(tax_2, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(coop, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(disallow, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(pagibig, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(phic, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(gsis, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(excess, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(total, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            AddCellToBody(tableLayout, "", "right");
        }


        private static PdfPTable addFooter(PdfPTable tableLayout,String preparedBy) {
            tableLayout.HeaderRows = 0;
            tableLayout.AddCell(new PdfPCell(new Phrase("1) Certified: Supporting documents complete and proper, Cash available ", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {

                Border = 0,
                Colspan = 7,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("2) Approved Payment:", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 7,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("3) Each employee whose name appears above has been", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("   Subject to ADA (where applicable)", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 7,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("    ", new Font(Font.FontFamily.HELVETICA, 8, 0, BaseColor.BLACK)))
            {
                Border = 0,
                Colspan = 7,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("   paid the amount indicated opposite his/her name", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            //NAMES
            tableLayout.AddCell(new PdfPCell(new Phrase("Angieline T. Adlaon, CPA, MBA", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {

                Border = 0,
                Colspan = 5,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3,
                PaddingTop = 20
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Sophia M. Mancao, MD, DPSP", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                Border = 0,
                Colspan = 5,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3,
                PaddingTop = 20
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Josephine D. Vergara", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                Border = 0,
                Colspan = 5,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3,
                PaddingTop = 20
            });

            tableLayout.AddCell(new PdfPCell(new Phrase(preparedBy, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                Border = 0,
                Colspan = 5,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3,
                PaddingTop = 20
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("Accountant III", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 5,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("OIC- Director III", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 5,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Administrative Officer V", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 5,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Prepared By", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 5,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });


            return tableLayout;
        }

        // Method to add single cell to the Header  
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            switch (cellText) {
                case "Net Amount":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Rowspan =2,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Name":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Colspan = 2,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Firstname":
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Colspan = 2,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Tardiness":
                case "Adjustment":
                case "Absences":
                case "W/Tax 10%":
                case "W/Tax 3%":
                case "W/Tax 2%":
                case "MO. RATE":
                case "HALF MO.":
                case "Coop":
                case "Disallow.":
                case "Pag-Ibig":
                case "PHIC":
                case "Position":
                case "GSIS":
                case "Excess Mobile":
                case "TIN":
                case "Total Amt.":
                case "Remarks":
                case "":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Deductions":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Colspan = 9,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,

                    });
                    break;
            }
        }
        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText,string position)
        {

            if (position.Equals("left"))
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                });
            }
            else {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                });
            }
        }

        public ActionResult PayrollPeriod()
        {
            return PartialView("payroll_period");
        }
    }
}